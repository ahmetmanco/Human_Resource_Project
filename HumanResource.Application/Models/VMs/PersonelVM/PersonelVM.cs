using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.PersonelVM
{
    public class PersonelVM
    {
        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
        public string FullName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }

    }
}
