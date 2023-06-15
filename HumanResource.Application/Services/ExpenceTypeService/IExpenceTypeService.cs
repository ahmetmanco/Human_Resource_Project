using HumanResource.Application.Models.DTOs.ExpenseTypeDTO;

namespace HumanResource.Application.Services.ExpenceTypeService
{
    public interface IExpenceTypeService
    {
        Task CreateDefault(int companyId);
        Task<bool> Create(CreateExpenseTypeDTO model, int? companyId);
        Task<bool> Update(UpdateExpenseTypeDTO model);
        Task Delete(int id);
        Task<UpdateExpenseTypeDTO> GetById(int id);
    }
}
