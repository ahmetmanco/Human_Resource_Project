using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.AdvanceVMs
{
    public class AdvanceDetailVM
    {
        public int Id { get; set; }
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Installments")]
        public int NumberOfInstallments { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Advance Date")]
        public string AdvanceDate { get; set; }
        [Display(Name = "Personel Name")]
        public string PersonelName { get; set; }
        [Display(Name ="Created Date")]
        public string CreatedDate { get; set; }
        [ValidateNever]
        public string Statu { get; set; }

    }
}
