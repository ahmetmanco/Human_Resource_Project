using AutoMapper;
using HumanResource.Application.Models.DTOs.LeaveDTO;
using HumanResource.Application.Models.VMs.CompanyManagerVMs;
using HumanResource.Application.Models.VMs.LeaveVM;
using HumanResource.Application.Models.VMs.PersonelVM;
using HumanResource.Application.Services.PersonelService;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositries;
using Microsoft.EntityFrameworkCore;

namespace HumanResource.Application.Services.LeaveServices
{
    public class LeaveService : ILeaveService
    {
        private readonly IPersonelService _personelService;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserRepository _appUserRepository;


        public LeaveService(ILeaveRepository leaveRepository, IMapper mapper, IPersonelService personelService, IAppUserRepository appUserRepository)
        {
            _leaveRepository = leaveRepository;
            _mapper = mapper;
            _personelService = personelService;
            _appUserRepository = appUserRepository;
        }

        public async Task<ProcessVM> Approve(int id)
        {
            Leave leave = await _leaveRepository.GetDefault(x => x.Id == id);
            leave.StatuId = Status.Approved.GetHashCode();
            var user = await _appUserRepository.GetDefault(x => x.Id == leave.UserId);
            return new ProcessVM() { Result = await _leaveRepository.Update(leave), UserEmail = user.Email };
        }

        public async Task<RequestVM> Create(CreateLeaveDTO model, string userName)
        {
            model.StatuId = Status.Awating_Approval.GetHashCode();
            Leave leave = _mapper.Map<Leave>(model);
            leave.UserId = await _personelService.GetPersonelId(userName);
            RequestVM result = new RequestVM();

            result.Result = await _leaveRepository.Add(leave);
            if (result.Result)
            {

                var user = await _appUserRepository.GetFilteredFirstOrDefault
                    (
                    select: x => new RequestVM
                    {
                        EmployeeName = x.FirstName + " " + x.LastName,
                        ManagerEmail = x.Manager.Email
                    },
                    where: x=>x.UserName == userName,
                    orderby: null,
                    include: x=>x.Include(x=>x.Manager)
                    );
                result.EmployeeName = user.EmployeeName;
                result.ManagerEmail = user.ManagerEmail;

                var leaves = await _leaveRepository.GetFilteredFirstOrDefault(
                    select: x => new Leave
                    {
                        Id = x.Id
                    },
                    where: x => x.User.UserName == userName,
                    orderby: x => x.OrderByDescending(y => y.CreatedDate),
                    include: x => x.Include(x => x.User)
                    );
                result.RequestId = leaves.Id;

            }

            return result;
        }

        public async Task Delete(int id)
        {
            Leave leave = await _leaveRepository.GetDefault(x => x.Id == id);
            if (leave != null)
            {
                leave.StatuId = Status.Deleted.GetHashCode();
                leave.DeletedDate = DateTime.Now;
                await _leaveRepository.Delete(leave);
            }
        }

        public async Task<UpdateLeaveDTO> GetById(int id)
        {
            Leave leave = await _leaveRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateLeaveDTO>(leave);
        }

        public async Task<List<LeaveVM>> GetLeavesForPersonel(Guid id)
        {
            var comments = await _leaveRepository.GetFilteredList(
                select: x => new LeaveVM()
                {
                    Id = x.Id,
                    StartDate = x.StartDate.ToShortDateString(),
                    CreatedDate = x.CreatedDate.ToShortDateString(),
                    EndDate = x.EndDate.ToShortDateString(),
                    ReturnDate = x.ReturnDate.ToShortDateString(),
                    LeavePeriod = x.LeavePeriod,
                    LeaveType = x.LeaveType.Name,
                    ManagerName = x.User.Manager.FirstName + " " + x.User.Manager.LastName,
                    Statu = x.Statu.Name

                },
                
                where: x => x.User.Id == id && x.StatuId != Status.Deleted.GetHashCode(),
                orderby: x => x.OrderByDescending(x => x.CreatedDate),
                include: x => x.Include(x => x.User).Include(x => x.LeaveType).Include(x=>x.Statu).Include(x=>x.User.Manager)
                );

            return comments;
        }

        public async Task<LeaveDetailVM> LeaveDetail(int id)
        {
            LeaveDetailVM advance = await _leaveRepository.GetFilteredFirstOrDefault(
                select: x => new LeaveDetailVM()
                {
                    Id = x.Id,
                    StartDate = x.StartDate.ToShortDateString(),
                    EndDate = x.EndDate.ToShortDateString(),
                    ReturnDate = x.ReturnDate.ToShortDateString(),
                    LeavePeriod = x.LeavePeriod,
                    LeaveType = x.LeaveType.Name,
                    PersonelName = x.User.FirstName + " " + x.User.LastName,
                    CreatedDate = x.CreatedDate.ToShortDateString(),
                    Statu = x.Statu.Name
                },
                where: x => x.Id == id,
                orderby: null,
                include: x => x.Include(x => x.User).Include(x=>x.LeaveType).Include(x => x.Statu)
                );
            return advance;
        }

        public async Task<ProcessVM> Reject(int id)
        {
            Leave leave = await _leaveRepository.GetDefault(x => x.Id == id);
            leave.StatuId = Status.Rejected.GetHashCode();
            var user = await _appUserRepository.GetDefault(x => x.Id == leave.UserId);
            return new ProcessVM() { Result = await _leaveRepository.Update(leave), UserEmail = user.Email };
        }

        public async Task<RequestVM> Update(UpdateLeaveDTO model)
        {
            Leave leave = _mapper.Map<Leave>(model);

            RequestVM result = new RequestVM();

            result.Result = await _leaveRepository.Update(leave);
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
