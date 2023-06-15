using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Application.Models.VMs.LeaveVM
{
    public enum LeaveTypeEnumVM
    {
        [Display(Name = "Annual Leave")]
        AnnualLeave = 1,
        [Display(Name = "Maternity Leave")]
        MaternityLeave,
        [Display(Name = "Paternity Leave")]
        PaternityLeave,
        [Display(Name = "Pregnancy Control Leave")]
        PregnancyControlLeave,
        [Display(Name = "Death Warrant")]
        DeathWarrant,
        [Display(Name = "New Job Search Permit")]
        NewJobSearchPermit,
        [Display(Name = "Marriage Permission")]
        MarriagePermission,
        [Display(Name = "Paid Leave")]
        PaidLeave

    }
}
