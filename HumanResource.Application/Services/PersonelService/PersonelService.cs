using HumanResource.Application.Models.VMs.PersonelVM;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositories;
using HumanResource.Domain.Repositries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResource.Application.Services.PersonelService
{
    public class PersonelService : IPersonelService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IAdvanceRepository _advanceRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpenseRepository _expenseRepository;

        public PersonelService(IAppUserRepository userRepository, ILeaveRepository leaveRepository, IAdvanceRepository advanceRepository, UserManager<AppUser> userManager, IExpenseRepository expenseRepository)
        {
            _userRepository = userRepository;
            _leaveRepository = leaveRepository;
            _advanceRepository = advanceRepository;
            _userManager = userManager;
            _expenseRepository = expenseRepository;
        }

        public async Task<PersonelVM> GetPersonel(string userName)
        {
            var personel = await _userRepository.GetFilteredFirstOrDefault(
               select: x => new PersonelVM()
               {
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   Image = x.ImagePath == null ? "/media/images/noImage.png" : x.ImagePath,
                   FullName = x.FirstName + " " + x.LastName,
                   CompanyId = x.CompanyId,
                   CompanyName = x.Company.CompanyName,
                   CompanyLogo = x.Company.ImagePath
               },
               where: x => x.UserName == userName,
               orderby: null,
               include: x => x.Include(x => x.Department).Include(x=>x.Company)
               );

            return personel;
        }

        public async Task<List<PersonelAdvanceRequestsVM>> GetPersonelAdvanceRequests(string name)
        {
            var personelAdvanceRequests = await _advanceRepository.GetFilteredList(
               select: x => new PersonelAdvanceRequestsVM()
               {
                   Id = x.Id,
                   Amount = x.Amount,
                   NumberOfInstallments = x.NumberOfInstallments,
                   CreatedDate = x.CreatedDate.ToShortDateString(),

               },
               where: x => x.User.UserName == name && x.StatuId == Status.Awating_Approval.GetHashCode(),
               orderby: x => x.OrderByDescending(x => x.CreatedDate),
               include: x => x.Include(x => x.User)
               ); ;

            return personelAdvanceRequests;

        }

        public async Task<List<PersonelExpenseRequestsVM>> GetPersonelExpenseRequests(string name)
        {
            var personelExpenseRequests = await _expenseRepository.GetFilteredList(
             select: x => new PersonelExpenseRequestsVM()
             {
                 Id = x.Id,
                 ExpenseDate = x.ExpenseDate.ToShortDateString(),
                 Amount = x.Amount,
                 CurrencyType = x.CurrencyType.Name,
                 ShortDescription = x.ShortDescription,
             },
             where: x => x.User.UserName == name && x.StatuId == Status.Awating_Approval.GetHashCode(),
             orderby: x => x.OrderByDescending(x => x.CreatedDate),
             include: x => x.Include(x => x.ExpenseType).Include(x => x.User).Include(x=>x.CurrencyType)
             );

            return personelExpenseRequests;
        }

        public async Task<Guid> GetPersonelId(string name)
        {
            AppUser user = await _userManager.FindByNameAsync(name);
            return user.Id;

        }

        public async Task<List<PersonelLeaveRequestsVM>> GetPersonelLeaveRequests(string name)
        {
            var personelLeaveRequests = await _leaveRepository.GetFilteredList(
              select: x => new PersonelLeaveRequestsVM()
              {
                  Id = x.Id,
                  StartDate = x.StartDate.ToShortDateString(),
                  EndDate = x.EndDate.ToShortDateString(),
                  LeavePeriod = x.LeavePeriod,
                  ReturnDate = x.ReturnDate.ToShortDateString(),
                  LeaveType = x.LeaveType.Name

              },
              where: x => x.User.UserName == name && x.StatuId == Status.Awating_Approval.GetHashCode(),
              orderby: x => x.OrderByDescending(x => x.CreatedDate),
              include: x => x.Include(x => x.LeaveType).Include(x => x.User)
              );

            return personelLeaveRequests;
        }

        public async Task<List<CompanyEmployeesVM>> GetCompanyEmployees(int companyId, string searchString)
        {
            List<CompanyEmployeesVM> companyEmployees = await _userRepository.GetFilteredList(
                select: x => new CompanyEmployeesVM()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Title = x.Title.Name,
                    Department = x.Department.Name,
                    ImagePath = x.ImagePath
                },
                where: x => x.CompanyId == companyId && ((x.FirstName + " " + x.LastName).Contains(searchString)),
                orderby: x => x.OrderBy(x => x.FirstName),
                include: x => x.Include(x => x.Title).Include(x=>x.Department)
                );

            return companyEmployees;
        }

    }
}
