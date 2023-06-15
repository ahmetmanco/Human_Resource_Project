using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.PersonelVM
{
    public class PersonelExpenseRequestsVM
    {
        public int Id { get; set; }

        [Display(Name = "Expense Date")]
        public string ExpenseDate { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Expense Currency Type")]
        public int CurrencyTypeId { get; set; }
        [Display(Name = "Currency Type")]
        public string CurrencyType { get; set; }

        [Display(Name = "Expense Short Description")]
        public string ShortDescription { get; set; }

    }
}
