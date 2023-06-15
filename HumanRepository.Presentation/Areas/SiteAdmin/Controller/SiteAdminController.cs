using Bogus.DataSets;
using HumanResource.Application.Models.VMs.EmailVM;
using HumanResource.Application.Services.AddressService;
using HumanResource.Application.Services.CompanyManagerService;
using HumanResource.Application.Services.EmailSenderService;
using HumanResource.Application.Services.PersonelService;
using HumanResource.Application.Services.SiteAdminService;
using HumanResource.Domain.Entities;
using HumanResource.Presentation.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HumanResource.Presentation.Areas.SiteAdmin
{
    [Area("SiteAdmin")]
    [Authorize(Roles = "SiteAdmin")]
    public class SiteAdminController : Controller
    {
        private readonly ISiteAdminService _siteAdminService;
        private readonly IPersonelService _personelService;
        private readonly ICompanyManagerService _companyManagerService;
        private readonly IEmailService _emailService;
        private readonly IAddressService _addressService;

        public SiteAdminController(ISiteAdminService siteAdminService, IPersonelService personelService, ICompanyManagerService companyManagerService, IEmailService emailService, IAddressService addressService)
        {
            _siteAdminService = siteAdminService;
            _personelService = personelService;
            _companyManagerService = companyManagerService;
            _emailService = emailService;
            _addressService = addressService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _siteAdminService.GetCompanyManagerRequests());
        }
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _siteAdminService.Approve(id);
            if (result.Result)
            {
				var conformationLink = Url.Action("Login", "Account", new { Area = "" }, Request.Scheme);
				TempData["success"] = "Company register request was approved.";
                var message = new Message(result.UserEmail, "Company Request", $"Your company request was approved by SiteAdmin. You can login by <a href={conformationLink!}>clicking here!</a>");
                _emailService.SendEmail(message);
                return RedirectToAction("index", "siteadmin", new { Area = "siteadmin" });
            }
            TempData["error"] = "There is something wrong. Request could not approved.";
            
            return RedirectToAction("index", "siteadmin", new { Area = "siteadmin" });
        }

        public async Task<IActionResult> Reject(int id)
        {
            var result = await _siteAdminService.Reject(id);
            if (result.Result)
            {
                TempData["success"] = "Company register request was approved.";
                var message = new Message(result.UserEmail, "Company Request", $"Your company request was rejected by SiteAdmin.");
                _emailService.SendEmail(message);
                return RedirectToAction("index", "siteadmin", new { Area = "siteadmin" });
            }
            TempData["error"] = "There is something wrong. Request could not rejected.";
            return RedirectToAction("index", "siteadmin", new { Area = "siteadmin" });
        }


        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _siteAdminService.GetCompanyDetails(id));
        }
        public async Task<IActionResult> List()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _siteAdminService.GetCompanies());
        }
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.Data = JsonConvert.SerializeObject(await _siteAdminService.CompaniesDistributionBySectors());
            ViewBag.Data2 = JsonConvert.SerializeObject(await _siteAdminService.CompaniesDistributionByStatus());
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View();
        }
    }
}
