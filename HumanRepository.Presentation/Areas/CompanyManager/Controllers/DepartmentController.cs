using HumanResource.Application.Models.DTOs.DepartmentDTOs;
using HumanResource.Application.Services.DepartmentService;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles ="CompanyManager")]
    public class DepartmentController : Controller
    {
        public readonly IDepartmentService _departmentService;
        public readonly IPersonelService _personelService;

        public DepartmentController(IDepartmentService departmentService, IPersonelService personelService)
        {
            _departmentService = departmentService;
            _personelService = personelService;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentDTO model)
        {

            if (ModelState.IsValid)
            {
                var personel = await _personelService.GetPersonel(User.Identity.Name);
                var result = await _departmentService.Create(model, personel.CompanyId);
                if (result)
                {
                    TempData["success"] = "Department was created successfully.";
                    return RedirectToAction("departments", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Leave request could not be created.";
                }
            }
            List<string> errors = new List<string>();
            foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
            {
                errors.Add(item.ErrorMessage);
            }
            TempData["modelError"] = errors.ToArray();
            return RedirectToAction("departments", "companymanager", new { Area = "companymanager" });
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _departmentService.GetById(id));
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateDepartmentDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentService.Update(model);
                if (result)
                {
                    TempData["success"] = "Department was updated successfully.";
                    return RedirectToAction("departments", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Department could not be created.";
                }
            }


            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            int id = int.Parse(collection["id"]);
            await _departmentService.Delete(id);
            TempData["success"] = "Department was deleted succesfully.";
            return RedirectToAction("departments", "companymanager", new { Area = "companymanager" });
        }
    }
}
