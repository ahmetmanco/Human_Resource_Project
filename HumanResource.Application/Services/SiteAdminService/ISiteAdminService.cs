using HumanResource.Application.Models.VMs.CompanyManagerVMs;
using HumanResource.Application.Models.VMs.CompanyVM;

namespace HumanResource.Application.Services.SiteAdminService
{
    public interface ISiteAdminService
    {
        Task<List<CompanyManagerRegisterRequestsVM>> GetCompanyManagerRequests();
        Task<List<CompanyVM>> GetCompanies();
        Task<ProcessVM> Approve(int id);
        Task<ProcessVM> Reject(int id);
        Task<CompanyDetailsVM> GetCompanyDetails(int? id);
        Task<List<CompanySectorPieVM>> CompaniesDistributionBySectors();
        Task<List<CompanyStatuPieVM>> CompaniesDistributionByStatus();
    }
}
