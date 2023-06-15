using HumanResource.Application.Services.AdvanceService;
using HumanResource.Application.Services.CompanyManagerService;
using HumanResource.Application.Services.ExpenseService;
using HumanResource.Application.Services.LeaveServices;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace HumanResource.Presentation.Areas.Personel.Controllers
{
    [Area("personel")]
    [Authorize(Roles = "CompanyManager, Employee")]
    public class PersonelController : Controller
    {
		private readonly ICompanyManagerService _companyManagerService;
		private readonly IPersonelService _personelService;
        private readonly IAdvanceService _advanceService;
        private readonly ILeaveService _leaveservice;
        private readonly IExpenseServices _expenseServices;
		public PersonelController(IPersonelService personelService, IAdvanceService advanceService, ILeaveService leaveservice, IExpenseServices expenseServices, ICompanyManagerService companyManagerService)
		{
			_personelService = personelService;
			_advanceService = advanceService;
			_leaveservice = leaveservice;
			_expenseServices = expenseServices;
			_companyManagerService = companyManagerService;
		}

		public async Task<IActionResult> Index()
        {
            ViewBag.AdvanceRequests = await _personelService.GetPersonelAdvanceRequests(User.Identity.Name);
            ViewBag.LeaveRequests = await _personelService.GetPersonelLeaveRequests(User.Identity.Name);
            ViewBag.ExpenseRequests = await _personelService.GetPersonelExpenseRequests(User.Identity.Name);
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View();
        }

        public async Task<IActionResult> Advances()
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _advanceService.GetAdvancesForPersonel(await _personelService.GetPersonelId(User.Identity.Name)));
        }

        public async Task<IActionResult> Leaves()
        {
			var personel = await _personelService.GetPersonel(User.Identity.Name);
			ViewBag.Personel = personel;
			ViewBag.LeaveTypes = new SelectList(await _companyManagerService.GetLeaveTypes(personel.CompanyId), "Id", "Name");
			return View(await _leaveservice.GetLeavesForPersonel(await _personelService.GetPersonelId(User.Identity.Name)));
        }

        public async Task<IActionResult> Expenses()
        {
			var personel = await _personelService.GetPersonel(User.Identity.Name);
			ViewBag.Personel = personel;
			ViewBag.ExpenseTypes = new SelectList(await _companyManagerService.GetExpenseTypes(personel.CompanyId), "Id", "Name");
			return View(await _expenseServices.GetExpenseForPersonel(await _personelService.GetPersonelId(User.Identity.Name)));
        }

        public async Task<IActionResult> Employees(int page = 1, string searchString = "")
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            var temp = await _personelService.GetCompanyEmployees((int)((ViewBag.Personel).CompanyId), searchString);
            

            return View(temp.ToPagedList(page, 3));
        }
    }
}
