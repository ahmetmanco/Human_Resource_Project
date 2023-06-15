using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HumanResource.Domain.Enums
{
    public enum LeaveTypes
    {
        [Display(Name = "Annual Leave")]
        Annual_Leave = 1,
        [Display(Name = "Maternity Leave")]
        Maternity_Leave,
        [Display(Name = "Paternity Leave")]
        Paternity_Leave,
        [Display(Name = "Pregnancy Control Leave")]
        Pregnancy_ControlLeave,
        [Display(Name = "Death Warrant")]
        Death_Warrant,
        [Display(Name = "New Job Search Permit")]
        New_Job_Search_Permit,
        [Display(Name = "Marriage Permission")]
        Marriage_Permission,
        [Display(Name = "Paid Leave")]
        Paid_Leave
    }
}
