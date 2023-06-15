using HumanResource.Application.Models.DTOs.AdvanceDTOs;
using HumanResource.Application.Models.VMs.AdvanceVMs;
using HumanResource.Application.Models.VMs.CompanyManagerVMs;
using HumanResource.Application.Models.VMs.PersonelVM;

namespace HumanResource.Application.Services.AdvanceService
{
    public interface IAdvanceService
	{
		Task<RequestVM> Create(CreateAdvanceDTO model,string userName);
		Task<RequestVM> Update(UpdateAdvanceDTO model);
		Task Delete(int id);
		Task<UpdateAdvanceDTO> GetById(int id);
		Task<AdvanceDetailVM> AdvanceDetail(int id);
		Task<List<AdvanceVM>> GetAdvancesForPersonel(Guid id);
		Task<ProcessVM> Approve(int id);
		Task<ProcessVM> Reject(int id);
	}
}
