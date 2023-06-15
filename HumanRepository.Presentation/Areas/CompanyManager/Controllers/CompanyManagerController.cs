using HumanResource.Application.Models.DTOs.CompanyManagerDTO;
using HumanResource.Application.Models.VMs.EmailVM;
using HumanResource.Application.Services.AccountServices;
using HumanResource.Application.Services.AddressService;
using HumanResource.Application.Services.AdvanceService;
using HumanResource.Application.Services.CompanyManagerService;
using HumanResource.Application.Services.EmailSenderService;
using HumanResource.Application.Services.ExpenseService;
using HumanResource.Application.Services.LeaveServices;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HumanResource.Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager, Manager")]
    public class CompanyManagerController : Controller
    {
        private readonly ICompanyManagerService _companyManagerService;
        private readonly IPersonelService _personelService;
        private readonly IAddressService _addressService;
        private readonly IEmailService _emailService;
        private readonly IAccountServices _accountService;
        private readonly IAdvanceService _advanceService;
        private readonly IExpenseServices _expenseService;
        private readonly ILeaveService _leaveService;

        public CompanyManagerController(ICompanyManagerService companyManagerService, IPersonelService personelService, IAddressService addressService, IEmailService emailService, IAccountServices accountService, IAdvanceService advanceService, IExpenseServices expenseService, ILeaveService leaveService)
		{
			_companyManagerService = companyManagerService;
			_personelService = personelService;
			_addressService = addressService;
			_emailService = emailService;
			_accountService = accountService;
			_advanceService = advanceService;
			_expenseService = expenseService;
			_leaveService = leaveService;
		}

		public async Task<IActionResult> Employees()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _companyManagerService.GetEmployees((int)((ViewBag.Personel).CompanyId)));
        }


        public async Task<IActionResult> Create()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyManagerService.CreateEmployee(model);
                if (!result.Errors.Any())
                {
                    if (result.Result.Succeeded)
                    {

                        var conformationLink = Url.Action("ConfirmEmail", "Account", new { token = result.Token, email = result.Email, Area = "" }, Request.Scheme);

                        var message = new Message(result.Email, "Conformation Email Link", $"Welcome to our human resources platform. Please <a href={conformationLink!}>click here</a> activate your account.   You can use your email and the password below to login to the platform.     {result.Password}");
                        _emailService.SendEmail(message);

                        return RedirectToAction("employees", "companymanager", new { Area = "companymanager" });
                    }
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item);
                }

            }
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            ViewBag.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            model.CityId = 0;
            model.CountryId = 0;
            model.DistrictId = 0;
            return View(model);
        }


        public async Task<IActionResult> Update(Guid id)
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            ViewBag.Countries = new SelectList(await _addressService.GetCountries(), "Id", "Name");
            var model = await _companyManagerService.GetByUserName(id);
            model.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyManagerService.UpdateEmployee(model);
                if (result.Succeeded)
                {
                    TempData["success"] = "Employee updated successfully";
                    return RedirectToAction("employees");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                } 
            }
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            ViewBag.Departments = new SelectList(await _companyManagerService.GetDepartments(personel.CompanyId), "Id", "Name");
            ViewBag.Titles = new SelectList(await _companyManagerService.GetTitles(personel.CompanyId), "Id", "Name");
            ViewBag.CompanyManagers = new SelectList(await _companyManagerService.GetCompanyManagers(personel.CompanyId), "Id", "FullName");
            ViewBag.Cities = new SelectList(await _addressService.GetCities(), "Id", "Name");
            ViewBag.Districts = new SelectList(await _addressService.GetDistricts(), "Id", "Name");
            model.BaseUrl = Request.Scheme + "://" + HttpContext.Request.Host.ToString();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            Guid id = Guid.Parse(collection["id"]);
            await _companyManagerService.Delete(id);
            TempData["success"] = "Employee was deleted succesfully.";
            return RedirectToAction("employees", "companymanager", new { Area = "companymanager" });
        }

        public async Task<IActionResult> Departments()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            return View(await _companyManagerService.GetDepartments(personel.CompanyId));
        }

        public async Task<IActionResult> LeaveTypes()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            return View(await _companyManagerService.GetLeaveTypes(personel.CompanyId));
        }

        public async Task<IActionResult> ExpenseTypes()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            return View(await _companyManagerService.GetExpenseTypes(personel.CompanyId));
        }


        public async Task<IActionResult> Titles()
        {
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
            return View(await _companyManagerService.GetTitles(personel.CompanyId));
        }

        public async Task<IActionResult> LeaveRequests()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _companyManagerService.GetPersonelLeaveRequests( await _personelService.GetPersonelId(User.Identity.Name)));
        }
        public async Task<IActionResult> AdvanceRequests()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _companyManagerService.GetPersonelAdvanceRequests(await _personelService.GetPersonelId(User.Identity.Name)));
        }
        public async Task<IActionResult> ExpenseRequests()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _companyManagerService.GetPersonelExpenseRequests(await _personelService.GetPersonelId(User.Identity.Name)));
        }

        public async Task<IActionResult> AdvanceRequestDetail(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            var advanceRequest = await _advanceService.AdvanceDetail(id);
            if (advanceRequest.Statu != "Deleted")
                return View(advanceRequest);
            return View("~/Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> ExpenseRequestDetail(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            var expenseRequest = await _expenseService.ExpenseDetail(id);
            if (expenseRequest.Statu != "Deleted")
                return View(expenseRequest);
            return View("~/Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> LeaveRequestDetail(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            var leaveRequest = await _leaveService.LeaveDetail(id);
            if (leaveRequest.Statu != "Deleted")
                return View(leaveRequest);
            return View("~/Views/Shared/Error.cshtml");
        }
		public async Task<IActionResult> DashboardExpenseType()
		{
            var personel = await _personelService.GetPersonel(User.Identity.Name);
            ViewBag.Personel = personel;
			ViewBag.ExpenseType = JsonConvert.SerializeObject(await _companyManagerService.ExpensesDistributionByExpensesType(personel.CompanyId));
			return View();
		}
	}
}
