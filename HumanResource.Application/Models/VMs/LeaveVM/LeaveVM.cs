using HumanResource.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.LeaveVM
{
    public class LeaveVM
    {
        public int Id { get; set; }

        [Display(Name = "Created Date")]
        public string CreatedDate { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Return Date")]
        public string ReturnDate { get; set; }

        [Display(Name = "Leave Period")]
        public string LeavePeriod { get; set; }
        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }
        [Display(Name = "Manager")]
        public string ManagerName { get; set; }
        [Display(Name ="Statu")]
        public string Statu { get; set; }

    }
}
