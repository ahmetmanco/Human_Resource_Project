using HumanResource.Application.Models.DTOs.TitleDTOs;
using HumanResource.Application.Services.DepartmentService;
using HumanResource.Application.Services.PersonelService;
using HumanResource.Application.Services.TitleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class TitleController : Controller
    {
        public readonly ITitleService _titleService;
        public readonly IPersonelService _personelService;

        public TitleController(ITitleService titleService, IPersonelService personelService)
        {
            _titleService = titleService;
            _personelService = personelService;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTitleDTO model)
        {
            if (ModelState.IsValid)
            {
                var personel = await _personelService.GetPersonel(User.Identity.Name);
                var result = await _titleService.Create(model, personel.CompanyId);
                if (result)
                {
                    TempData["success"] = "Title was created successfully.";
                    return RedirectToAction("titles", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Title could not be created.";
                }
            }
            List<string> errors = new List<string>();
            foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
            {
                errors.Add(item.ErrorMessage);
            }
            TempData["modelError"] = errors.ToArray();
            return RedirectToAction("titles", "companymanager", new { Area = "companymanager" });
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(await _titleService.GetById(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateTitleDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _titleService.Update(model);
                if (result)
                {
                    TempData["success"] = "Title was updated successfully.";
                    return RedirectToAction("titles", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Title could not be created.";
                }
            }

            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            int id = int.Parse(collection["id"]);
            await _titleService.Delete(id);
            TempData["success"] = "Title was deleted succesfully.";
            return RedirectToAction("titles", "companymanager", new { Area = "companymanager" });
        }
    }
}
