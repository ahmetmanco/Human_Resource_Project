using HumanResource.Application.Models.VMs.PersonelVM;

namespace HumanResource.Application.Services.PersonelService
{
    public interface IPersonelService
    {
        Task<List<PersonelLeaveRequestsVM>> GetPersonelLeaveRequests(string name);
        Task<List<PersonelAdvanceRequestsVM>> GetPersonelAdvanceRequests(string name);
        Task<List<PersonelExpenseRequestsVM>> GetPersonelExpenseRequests(string name);

        Task<PersonelVM> GetPersonel(string userName);
        Task<Guid> GetPersonelId(string name);
        Task<List<CompanyEmployeesVM>> GetCompanyEmployees(int companyId, string searchString);

    }
}
