using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.ExpenseVM
{
    public class ExpenseDetailVM
    {
        public int Id { get; set; }

        [Display(Name = "Expense Date")]
        public string ExpenseDate { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Currency Type")]
        public string CurrencyType { get; set; }

        [Display(Name = "Description")]
        public string ShortDescription { get; set; }
        [Display(Name = "Description Detail")]
        public string LongDescription { get; set; }

        [Display(Name = "Expense Type")]
        public string ExpenseType { get; set; }
        public string PersonelName { get; set; }
        [Display(Name = "Created Date")]
        public string CreatedDate { get; set; }
        [ValidateNever]
        public string Statu { get; set; }
    }
}
