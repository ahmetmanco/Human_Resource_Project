using HumanResource.Application.Models.DTOs.DepartmentDTOs;

namespace HumanResource.Application.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<bool> Create(CreateDepartmentDTO model, int? companyId);
        Task<bool> Update(UpdateDepartmentDTO model);
        Task Delete(int id);
        Task<UpdateDepartmentDTO> GetById(int id);
    }
}
