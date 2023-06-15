using System.ComponentModel.DataAnnotations;

namespace HumanResource.Domain.Enums
{
    public enum Status
    {
        [Display(Name ="Active")]
        Active = 1,
        [Display(Name = "Passive")]
        Passive,
        [Display(Name = "Awating Approval")]
        Awating_Approval,
        [Display(Name = "Approved")]
        Approved,
        [Display(Name = "Deleted")]
        Deleted,
        [Display(Name = "Rejected")]
        Rejected
    }
}
