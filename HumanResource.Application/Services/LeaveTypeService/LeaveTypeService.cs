using AutoMapper;
using HumanResource.Application.Models.DTOs.LeaveTypeDTO;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositries;

namespace HumanResource.Application.Services.LeaveTypeService
{
    internal class LeaveTypeService : ILeaveTypeService
    {

        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<bool> Create(CreateLeaveTypeDTO model, int? companyId)
        {
            LeaveType leaveType = _mapper.Map<LeaveType>(model);
            leaveType.StatuId = Status.Active.GetHashCode();
            leaveType.CompanyId = companyId;
            return await _leaveTypeRepository.Add(leaveType);
        }

        public async Task CreateDefault(int companyId)
        {
            foreach (var item in Enum.GetValues(typeof(LeaveTypes)))
            {

                LeaveType leaveType = new LeaveType();
                leaveType.Name = String.Join(" ", item.ToString().Split("_"));
                leaveType.CompanyId = companyId;
                leaveType.StatuId = Status.Active.GetHashCode();
                await _leaveTypeRepository.Add(leaveType);
            }
        }

        public async Task Delete(int id)
        {
            LeaveType leaveType = await _leaveTypeRepository.GetDefault(x => x.Id == id);
            if (leaveType != null)
            {
                leaveType.StatuId = Status.Deleted.GetHashCode();
                await _leaveTypeRepository.Delete(leaveType);
            }
        }

        public async Task<UpdateLeaveTypeDTO> GetById(int id)
        {
            LeaveType leaveType = await _leaveTypeRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateLeaveTypeDTO>(leaveType);

        }

        public async Task<bool> Update(UpdateLeaveTypeDTO model)
        {
            LeaveType leaveType = _mapper.Map<LeaveType>(model);
            return await _leaveTypeRepository.Update(leaveType);
        }
    }
}
