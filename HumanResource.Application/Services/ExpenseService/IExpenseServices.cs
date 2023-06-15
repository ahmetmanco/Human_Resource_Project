using HumanResource.Application.Models.DTOs.ExpenseDTO;
using HumanResource.Application.Models.VMs.CompanyManagerVMs;
using HumanResource.Application.Models.VMs.ExpenseVM;
using HumanResource.Application.Models.VMs.PersonelVM;

namespace HumanResource.Application.Services.ExpenseService
{
    public interface IExpenseServices
    {
        Task<RequestVM> Create(CreateExpenseDTO model, string userName);
        Task<RequestVM> Update(UpdateExpenseDTO model);
        Task Delete(int id);
        Task<UpdateExpenseDTO> GetById(int id);
        Task<List<ExpenseVM>> GetExpenseForPersonel(Guid id);
        Task<ExpenseDetailVM> ExpenseDetail(int id);
        Task<ProcessVM> Approve(int id);
        Task<ProcessVM> Reject(int id);
    }
}
