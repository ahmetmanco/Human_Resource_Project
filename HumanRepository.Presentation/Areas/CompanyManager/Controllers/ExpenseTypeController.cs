using HumanResource.Application.Models.DTOs.ExpenseTypeDTO;
using HumanResource.Application.Services.ExpenceTypeService;
using HumanResource.Application.Services.PersonelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Presentation.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Authorize(Roles = "CompanyManager")]
    public class ExpenseTypeController : Controller
    {
        public readonly IExpenceTypeService _expenseTypeService;
        public readonly IPersonelService _personelService;

        public ExpenseTypeController(IExpenceTypeService expenseTypeService, IPersonelService personelService)
        {
            _expenseTypeService = expenseTypeService;
            _personelService = personelService;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExpenseTypeDTO model)
        {

            if (ModelState.IsValid)
            {
                var personel = await _personelService.GetPersonel(User.Identity.Name);
                var result = await _expenseTypeService.Create(model, personel.CompanyId);
                if (result)
                {
                    TempData["success"] = "Expense type was created successfully.";
                    return RedirectToAction("expensetypes", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Expense Type could not be created.";
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
            return View(await _expenseTypeService.GetById(id));
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateExpenseTypeDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _expenseTypeService.Update(model);
                if (result)
                {
                    TempData["success"] = "Expense type was updated successfully.";
                    return RedirectToAction("expensetypes", "companymanager", new { Area = "companymanager" });
                }
                else
                {
                    TempData["error"] = "Something goes wrong, Expense type could not be created.";
                }
            }


            ViewBag.Personel = await _personelService.GetPersonel(User.Identity.Name);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection collection)
        {
            int id = int.Parse(collection["id"]);
            await _expenseTypeService.Delete(id);
            TempData["success"] = "Expense type was deleted succesfully.";
            return RedirectToAction("expensetypes", "companymanager", new { Area = "companymanager" });
        }
    }
}
