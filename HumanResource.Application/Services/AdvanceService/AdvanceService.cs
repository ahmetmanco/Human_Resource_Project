using AutoMapper;
using HumanResource.Application.Models.DTOs.AdvanceDTOs;
using HumanResource.Application.Models.VMs.AdvanceVMs;
using HumanResource.Application.Models.VMs.CompanyManagerVMs;
using HumanResource.Application.Models.VMs.LeaveVM;
using HumanResource.Application.Models.VMs.PersonelVM;
using HumanResource.Application.Services.PersonelService;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositries;
using HumanResource.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResource.Application.Services.AdvanceService
{
    public class AdvanceService : IAdvanceService
    {
        private readonly IAdvanceRepository _advanceRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IPersonelService _personelService;

        public AdvanceService(IAdvanceRepository advanceRepository, IMapper mapper, IAppUserRepository appUserRepository, IPersonelService personelService)
        {
            _advanceRepository = advanceRepository;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _personelService = personelService;
        }

        public async Task<AdvanceDetailVM> AdvanceDetail(int id)
        {
            AdvanceDetailVM advance = await _advanceRepository.GetFilteredFirstOrDefault(
                select: x => new AdvanceDetailVM()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    NumberOfInstallments = x.NumberOfInstallments,
                    Description = x.Description,
                    AdvanceDate = x.AdvanceDate.ToShortDateString(),
                    PersonelName = x.User.FirstName + " " + x.User.LastName,
                    CreatedDate = x.CreatedDate.ToShortDateString(),
                    Statu = x.Statu.Name
                },
                where: x => x.Id == id,
                orderby: null,
                include: x => x.Include(x => x.User).Include(x=>x.Statu)
                );
            return advance;
        }

        public async Task<ProcessVM> Approve(int id)
        {
            Advance advance = await _advanceRepository.GetDefault(x => x.Id == id);
            advance.StatuId = Status.Approved.GetHashCode();
            var user = await _appUserRepository.GetDefault(x => x.Id == advance.UserId);
            return new ProcessVM() { Result = await _advanceRepository.Update(advance), UserEmail = user.Email };
        }

        public async Task<RequestVM> Create(CreateAdvanceDTO model, string userName)
        {
            model.StatuId = Status.Awating_Approval.GetHashCode();
            Advance advance = _mapper.Map<Advance>(model);
            advance.UserId = await _personelService.GetPersonelId(userName);
            RequestVM result = new RequestVM();

            result.Result = await _advanceRepository.Add(advance);
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

                var advances = await _advanceRepository.GetFilteredFirstOrDefault(
                    select: x => new Advance
                    {
                        Id = x.Id
                    },
                    where: x => x.User.UserName == userName,
                    orderby: x => x.OrderByDescending(y => y.CreatedDate),
                    include: x => x.Include(x => x.User)
                    );
                result.RequestId = advances.Id;

            }

            return result;
        }


        public async Task Delete(int id)
        {
            Advance advance = await _advanceRepository.GetDefault(x => x.Id == id);
            if (advance != null)
            {
                advance.StatuId = Status.Deleted.GetHashCode();
                advance.DeletedDate = DateTime.Now;
                await _advanceRepository.Delete(advance);
            }
        }

        public async Task<List<AdvanceVM>> GetAdvancesForPersonel(Guid id)
        {
            var advances = await _advanceRepository.GetFilteredList(
                select: x => new AdvanceVM()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    NumberOfInstallments = x.NumberOfInstallments,
                    AdvanceDate = x.AdvanceDate.ToShortDateString(),
                    ManagerName = x.User.Manager.FirstName + " " + x.User.Manager.LastName,
                    Statu = x.Statu.Name
                },
                where: x => x.User.Id == id && x.StatuId != Status.Deleted.GetHashCode(),
                orderby: x => x.OrderByDescending(x => x.CreatedDate),
                include: x => x.Include(x => x.User).Include(x => x.Statu).Include(x => x.User.Manager)
                );
            return advances;
        }


        public async Task<UpdateAdvanceDTO> GetById(int id)
        {
            Advance advance = await _advanceRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateAdvanceDTO>(advance);
        }

        public async Task<ProcessVM> Reject(int id)
        {
            Advance advance = await _advanceRepository.GetDefault(x => x.Id == id);
            advance.StatuId = Status.Rejected.GetHashCode();
            var user = await _appUserRepository.GetDefault(x => x.Id == advance.UserId);
            return new ProcessVM() { Result = await _advanceRepository.Update(advance), UserEmail = user.Email };
        }

        public async Task<RequestVM> Update(UpdateAdvanceDTO model)
        {
            Advance advance = _mapper.Map<Advance>(model);


            RequestVM result = new RequestVM();

            result.Result = await _advanceRepository.Update(advance);
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
