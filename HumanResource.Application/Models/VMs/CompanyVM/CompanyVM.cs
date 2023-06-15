using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.CompanyVM
{
    public class CompanyVM
    {
        public Guid? UserId { get; set; }
        public int? CompanyId { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Statu")]
        public string Statu { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
