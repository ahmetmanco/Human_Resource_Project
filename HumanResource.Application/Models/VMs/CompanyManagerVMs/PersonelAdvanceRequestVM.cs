using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.CompanyManagerVMs
{
    public class PersonelAdvanceRequestVM
    {
        public int Id { get; set; }
        [Display(Name = "Personel Name")]
        public string PersonelFullName { get; set; }
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Installments")]
        public int NumberOfInstallments { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Advance Date")]
        public string AdvanceDate { get; set; }
    }
}
