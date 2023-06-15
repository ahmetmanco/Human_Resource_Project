using HumanResource.Application.Models.DTOs.AdvanceDTOs;
using HumanResource.Application.Models.VMs.EmailVM;
using HumanResource.Application.Services.AdvanceService;
using HumanResource.Application.Services.EmailSenderService;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Presentation.Areas.Personel.Controllers
{
    [Area("personel")]
    [Authorize(Roles = "CompanyManager, Employee")]
    public class AdvanceController : Controller
    {
        private readonly IAdvanceService _advanceService;
        private readonly IPersonelService _personelService;
        private readonly IEmailService _emailService;

        public AdvanceController(IPersonelService personelService, IAdvanceService advanceService, IEmailService emailService)
        {
            _personelService = personelService;
            _advanceService = advanceService;
            _emailService = emailService;
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdvanceDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _advanceService.Create(model, User.Identity.Name);
                if (result.Result)
                {

                    var conformationLink = Url.Action("AdvanceRequestDetail", "CompanyManager", new { id = result.RequestId, Area = "CompanyManager" }, Request.Scheme);

                    var message = new Message(result.ManagerEmail, $"New Advance Request", $"New Advance Request was created by {result.EmployeeName}. Please <a href={conformationLink!}>click here</a> to display advance request.");
                    _emailService.SendEmail(message);

                    TempData["success"] = "advance request was created successfully.";
                    return RedirectToAction("advances", "personel", new { Area = "personel" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Advance request could not be created.";
                }
            }
            List<string> errors = new List<string>();
            foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
            {
                errors.Add(item.ErrorMessage);
            }
            TempData["modelError"] = errors.ToArray();
            return RedirectToAction("advances", "personel", new { Area = "personel" });
        }


        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _advanceService.GetById(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateAdvanceDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _advanceService.Update(model);
                if (result.Result)
                {

                    var conformationLink = Url.Action("AdvanceRequestDetail", "CompanyManager", new { id = result.RequestId, Area = "CompanyManager" }, Request.Scheme);

                    var message = new Message(result.ManagerEmail, $"Updated Advance Request", $"Advance Request was updated by {result.EmployeeName}. Please <a href={conformationLink!}>click here</a> to display advance request.");
                    _emailService.SendEmail(message);

                    TempData["success"] = "advance request was updated successfully.";
                    return RedirectToAction("advances", "personel", new { Area = "personel" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Advance request could not be created.";
                }
            }


            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            int id = int.Parse(collection["id"]);
            await _advanceService.Delete(id);
            TempData["success"] = "Advance was deleted succesfully.";
            return RedirectToAction("advances", "personel", new { Area = "personel" });
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _advanceService.Approve(id);
            if (result.Result)
            {
                TempData["success"] = "Personel advance request was approved.";
                var message = new Message(result.UserEmail, "Advance Request", $"Your advance request was approved by your manager.");
                _emailService.SendEmail(message);
                return RedirectToAction("AdvanceRequests", "companymanager", new { Area = "companymanager" });
            }
            TempData["error"] = "There is something wrong. Request could not approved.";
            return RedirectToAction("AdvanceRequests", "companymanager", new { Area = "companymanager" });
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var result = await _advanceService.Reject(id);
            if (result.Result)
            {
                TempData["success"] = "Personel advance request was rejected.";
                var message = new Message(result.UserEmail, "Advance Request", $"Your advance request was rejected by your manager.");
                _emailService.SendEmail(message);
                return RedirectToAction("AdvanceRequests", "companymanager", new { Area = "companymanager" });
            }
            TempData["error"] = "There is something wrong. Request could not rejected.";
            return RedirectToAction("AdvanceRequests", "companymanager", new { Area = "companymanager" });
        }
    }
}
