using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.PersonelVM
{
    public enum BloodTypeEnumVM
    {
        [Display(Name = "A rh +")]
        Apositive = 1,
        [Display(Name = "A rh -")]
        Anegative,
        [Display(Name = "B rh +")]
        Bpositive,
        [Display(Name = "B rh -")]
        Bnegative,
        [Display(Name = "AB rh +")]
        ABpositive,
        [Display(Name = "AB rh -")]
        ABnegative,
        [Display(Name = "0 rh +")]
        ZeroPositive,
        [Display(Name = "0 rh -")]
        ZeroNegative,
    }
}
