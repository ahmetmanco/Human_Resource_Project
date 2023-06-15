using HumanResource.Application.Models.DTOs.DepartmentDTOs;
using HumanResource.Application.Models.DTOs.LeaveTypeDTO;
using HumanResource.Application.Services.LeaveTypeService;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class LeaveTypeController : Controller
    {
        public readonly ILeaveTypeService _leaveTypeService;
        public readonly IPersonelService _personelService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService, IPersonelService personelService)
        {
            _leaveTypeService = leaveTypeService;
            _personelService = personelService;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaveTypeDTO model)
        {

            if (ModelState.IsValid)
            {
                var personel = await _personelService.GetPersonel(User.Identity.Name);
                var result = await _leaveTypeService.Create(model, personel.CompanyId);
                if (result)
                {
                    TempData["success"] = "Leave Type was created successfully.";
                    return RedirectToAction("leavetypes", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Leave type could not be created.";
                }
            }
            List<string> errors = new List<string>();
            foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
            {
                errors.Add(item.ErrorMessage);
            }
            TempData["modelError"] = errors.ToArray();
            return RedirectToAction("leavetypes", "companymanager", new { Area = "companymanager" });
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _leaveTypeService.GetById(id));
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateLeaveTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _leaveTypeService.Update(model);
                if (result)
                {
                    TempData["success"] = "Leave type was updated successfully.";
                    return RedirectToAction("leavetypes", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Leave type could not be created.";
                }
            }


            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            int id = int.Parse(collection["id"]);
            await _leaveTypeService.Delete(id);
            TempData["success"] = "Leave type was deleted succesfully.";
            return RedirectToAction("leavetypes", "companymanager", new { Area = "companymanager" });
        }
    }
}
