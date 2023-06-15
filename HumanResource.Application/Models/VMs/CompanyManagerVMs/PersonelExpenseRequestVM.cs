using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.CompanyManagerVMs
{
    public class PersonelExpenseRequestVM
    {
        public int Id { get; set; }

        [Display(Name = "Personel Name")]
        public string PersonelFullName { get; set; }
        [Display(Name = "Expense Date")]
        public string ExpenseDate { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Currency Type")]
        public string CurrencyType { get; set; }

        [Display(Name = "Description")]
        public string ShortDescription { get; set; }

    }
}
