using HumanResource.Application.Models.DTOs.AccountDTO;
using HumanResource.Application.Models.VMs.EmailVM;
using HumanResource.Application.Services.AccountServices;
using HumanResource.Application.Services.AddressService;
using HumanResource.Application.Services.CompanyManagerService;
using HumanResource.Application.Services.EmailSenderService;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanResource.Presentation.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly IPersonelService _personelService;
        private readonly IAddressService _addressService;
        private readonly IEmailService _emailService;
        private readonly ICompanyManagerService _companyManagerService;

        public AccountController(IAccountServices accountServices, IPersonelService personelService, IAddressService addressService, IEmailService emailService, ICompanyManagerService companyManagerService)
        {
            _accountServices = accountServices;
            _personelService = personelService;
            _addressService = addressService;
            _emailService = emailService;
            _companyManagerService = companyManagerService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("login", "account");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View();
        }
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountServices.Register(model);
                if (result.Result.Succeeded)
                {
                    var message = new Message(result.Email, "Information e-mail", "Welcome to our human resources platform. Your request has been received. Notification will be made as soon as possible.");
                    _emailService.SendEmail(message);

                    TempData["Information"] = "Please check your mailbox!";

                    return RedirectToAction("login", "account");
                }


            }
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("index", "personel", new { Area = "personel" });

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountServices.Login(model);

                if (result.Succeeded)
                {
                    if (await _accountServices.IsAdmin(model.UserNameOrEmail))
                        return RedirectToAction("index", "siteadmin", new { Area = "SiteAdmin" });
                    return RedirectToLocal(returnUrl);
                }

                if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
                    TempData["loginError"] = "Username, Email or Password is wrong.";
                else if (result == Microsoft.AspNetCore.Identity.SignInResult.NotAllowed)
                    TempData["loginError"] = "Email has not been verified yet. Please verify your email.";
                else
                    TempData["loginError"] = "Invalid Login Attemp";

            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _accountServices.LogOut();
            return RedirectToAction("login");
        }
        public async Task<IActionResult> Profile()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            var model = await _accountServices.GetByUserName(User.Identity.Name);
            model.ImagePath = personel.Image;
            model.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UpdateProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                await _accountServices.UpdateUser(model);
                TempData["success"] = "Profile was updated successfully.";
                return RedirectToAction("profile");
            }
            TempData["error"] = "Something goes wrong.";
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> setDropDownListCity(int id)
        {
            var cities = await _addressService.GetCities(id);
            return Json(cities);
        }
        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> setDropDownListDistrict(int id)
        {
            var districts = await _addressService.GetDistricts(id);
            return Json(districts);
        }


        private IActionResult RedirectToLocal(string returnUrl = "/")
        {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("index", "");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var result = await _accountServices.ConfirmEmail(token, email);

            if (result.Succeeded)
            {
                TempData["SuccessConformation"] = "Your account was confirmed successfully.";
                return RedirectToAction("Login");

            }
            TempData["ErrorConformation"] = "Your account could not confirmed. Please register again.";
            return RedirectToAction("register");

        }
    }
}
