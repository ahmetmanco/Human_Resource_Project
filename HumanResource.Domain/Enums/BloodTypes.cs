using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HumanResource.Domain.Enums
{
    public enum BloodTypes
    {
        [Display(Name = "A rh +")]
        A_positive = 1,
        [Display(Name = "A rh -")]
        A_negative,
        [Display(Name = "B rh +")]
        B_positive,
        [Display(Name = "B rh -")]
        B_negative,
        [Display(Name = "AB rh +")]
        AB_positive,
        [Display(Name = "AB rh -")]
        AB_negative,
        [Display(Name = "0 rh +")]
        Zero_Positive,
        [Display(Name = "0 rh -")]
        Zero_Negative,

    }
}
