using HumanResource.Application.Models.DTOs.CompanyDTO;
using HumanResource.Application.Services.AccountServices;
using HumanResource.Application.Services.AddressService;
using HumanResource.Application.Services.CompanyManagerService;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanResource.Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class CompanyController : Controller
    {
        private readonly IPersonelService _personelService;
        private readonly ICompanyManagerService _companyManagerService;
        private readonly IAddressService _addressService;
        private readonly IAccountServices _accountService;

		public CompanyController(IPersonelService personelService, ICompanyManagerService companyManagerService, IAddressService addressService, IAccountServices accountService)
		{
			_personelService = personelService;
			_companyManagerService = companyManagerService;
			_addressService = addressService;
			_accountService = accountService;
		}

		public async Task<IActionResult> Company()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
			ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
			ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
			ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(await _companyManagerService.GetCompany(personel.CompanyId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Company(UpdateCompanyDTO model)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _companyManagerService.UpdateCompany(model);
                if (result == true)
                {
                    TempData["success"] = "Company was updated successfully.";
                    return RedirectToAction("company", "company", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Company could not be created.";
                }
            }
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }
    }
}
