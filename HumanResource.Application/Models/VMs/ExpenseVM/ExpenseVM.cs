using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.ExpenseVM
{
    public class ExpenseVM
    {
        public int Id { get; set; }

        [Display(Name = "Expense Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMMM-yyyy}")]
        public string ExpenseDate { get; set; }

        [Display(Name = "Amount")]
        public Decimal Amount { get; set; }

        [Display(Name = "Currency Type")]
        public string CurrencyType { get; set; }

        [Display(Name = "Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Expense Type")]
        public string ExpenseType { get; set; }
        [Display(Name = "Manager")]
        public string ManagerName { get; set; }
        [Display(Name ="Statu")]
        public string Statu { get; set; }
    }
}
