using AutoMapper;
using HumanResource.Application.Models.DTOs.ExpenseDTO;
using HumanResource.Application.Models.VMs.CompanyManagerVMs;
using HumanResource.Application.Models.VMs.ExpenseVM;
using HumanResource.Application.Models.VMs.PersonelVM;
using HumanResource.Application.Services.PersonelService;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositories;
using HumanResource.Domain.Repositries;
using Microsoft.EntityFrameworkCore;

namespace HumanResource.Application.Services.ExpenseService
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IPersonelService _personelService;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;
        public ExpenseServices(IPersonelService personelService, IExpenseRepository expenseRepository, IMapper mapper, IAppUserRepository appUserRepository)
        {
            _personelService = personelService;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
        }

        public async Task<ProcessVM> Approve(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.Id == id);
            expense.StatuId = Status.Approved.GetHashCode();
            var user = await _appUserRepository.GetDefault(x => x.Id == expense.UserId);
            return new ProcessVM() { Result = await _expenseRepository.Update(expense), UserEmail = user.Email };
        }

        public async Task<RequestVM> Create(CreateExpenseDTO model, string userName)
        {
            model.StatuId = Status.Awating_Approval.GetHashCode();
            Expense expense = _mapper.Map<Expense>(model);
            expense.UserId = await _personelService.GetPersonelId(userName);


            RequestVM result = new RequestVM();

            result.Result = await _expenseRepository.Add(expense);
            if (result.Result)
            {

                var user = await _appUserRepository.GetFilteredFirstOrDefault
                    (
                    select: x => new RequestVM
                    {
                        EmployeeName = x.FirstName + " " + x.LastName,
                        ManagerEmail = x.Manager.Email
                    },
                    where: x => x.UserName == userName,
                    orderby: null,
                    include: x => x.Include(x => x.Manager)
                    );
                result.EmployeeName = user.EmployeeName;
                result.ManagerEmail = user.ManagerEmail;

                var expenses = await _expenseRepository.GetFilteredFirstOrDefault(
                    select: x => new Expense
                    {
                        Id = x.Id
                    },
                    where: x => x.User.UserName == userName,
                    orderby: x => x.OrderByDescending(y => y.CreatedDate),
                    include: x => x.Include(x => x.User)
                    );
                result.RequestId = expenses.Id;

            }

            return result;
        }

        public async Task Delete(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.Id == id);
            if (expense == null)
            {
                throw new Exception("No expenditure has been entered!");
            }
            else
            {
                expense.StatuId = Status.Deleted.GetHashCode();
                expense.DeletedDate = DateTime.Now;
                await _expenseRepository.Delete(expense);
            }
        }

        public async Task<ExpenseDetailVM> ExpenseDetail(int id)
        {
            ExpenseDetailVM expense = await _expenseRepository.GetFilteredFirstOrDefault(
                select: x => new ExpenseDetailVM()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    ShortDescription = x.ShortDescription,
                    LongDescription = x.LongDescription,
                    CurrencyType = x.CurrencyType.Name,
                    ExpenseType = x.ExpenseType.Name,
                    ExpenseDate = x.ExpenseDate.ToShortDateString(),
                    PersonelName = x.User.FirstName + " " + x.User.LastName,
                    CreatedDate = x.CreatedDate.ToShortDateString(),
                    Statu = x.Statu.Name
                },
                where: x => x.Id == id,
                orderby: null,
                include: x => x.Include(x => x.User).Include(x=>x.CurrencyType).Include(x=>x.ExpenseType).Include(x => x.Statu)
                );
            return expense;
        }

        public async Task<UpdateExpenseDTO> GetById(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateExpenseDTO>(expense);
        }

        public async Task<List<ExpenseVM>> GetExpenseForPersonel(Guid id)
        {
            var expenses = await _expenseRepository.GetFilteredList(
                select: x => new ExpenseVM()
                {
                    Id = x.Id,
                    ExpenseDate = x.ExpenseDate.ToShortDateString(),                   
                    Amount = x.Amount,
                    CurrencyType = x.CurrencyType.Name,
                    ShortDescription = x.ShortDescription,
                    ExpenseType = x.ExpenseType.Name,
                    ManagerName = x.User.Manager.FirstName + " " + x.User.Manager.LastName,
                    Statu = x.Statu.Name
                },
                where: x => x.User.Id == id && x.StatuId != Status.Deleted.GetHashCode(),
                orderby: x => x.OrderByDescending(x => x.CreatedDate),
                include: x => x.Include(x => x.User).Include(x => x.ExpenseType).Include(x => x.CurrencyType).Include(x => x.User.Manager).Include(x=>x.Statu)
                );

            return expenses;
        }

        public async Task<ProcessVM> Reject(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.Id == id);
            expense.StatuId = Status.Rejected.GetHashCode();
            var user = await _appUserRepository.GetDefault(x => x.Id == expense.UserId);
            return new ProcessVM() { Result = await _expenseRepository.Update(expense), UserEmail = user.Email };
        }

        public async Task<RequestVM> Update(UpdateExpenseDTO model)
        {
            Expense expense = _mapper.Map<Expense>(model);
            RequestVM result = new RequestVM();
            result.Result = await _expenseRepository.Update(expense);
            if (result.Result)
            {

                var user = await _appUserRepository.GetFilteredFirstOrDefault
                    (
                    select: x => new RequestVM
                    {
                        EmployeeName = x.FirstName + " " + x.LastName,
                        ManagerEmail = x.Manager.Email
                    },
                    where: x => x.Id == model.UserId,
                    orderby: null,
                    include: x => x.Include(x => x.Manager)
                    );
                result.EmployeeName = user.EmployeeName;
                result.ManagerEmail = user.ManagerEmail;
                result.RequestId = model.Id;

            }

            return result;
        }
    }
}
