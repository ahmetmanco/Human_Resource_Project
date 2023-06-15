using HumanResource.Application.Models.DTOs.LeaveTypeDTO;

namespace HumanResource.Application.Services.LeaveTypeService
{
    public interface ILeaveTypeService
    {
        Task CreateDefault(int companyId);
        Task<bool> Create(CreateLeaveTypeDTO model, int? companyId);
        Task<bool> Update(UpdateLeaveTypeDTO model);
        Task Delete(int id);
        Task<UpdateLeaveTypeDTO> GetById(int id);
    }
}
