using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.PersonelVM
{
    public class PersonelAdvanceRequestsVM
    {
        public int Id { get; set; }
        [Display(Name ="Miktar")]
        public decimal Amount { get; set; }

        [Display(Name ="Taksit miktarı")]
        public int NumberOfInstallments { get; set; }

        [Display(Name ="Oluşturulma Tarihi")]
        public  string CreatedDate { get; set; }

    }
}
