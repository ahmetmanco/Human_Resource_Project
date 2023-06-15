using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.CompanyManagerVMs
{
    public class PersonelLeaveRequestVM
    {
        public int Id { get; set; }

        [Display(Name ="Personel Name")]
        public string PersonelFullName{ get; set; }
        [Display(Name ="Leave Type")]
        public string LeaveType { get; set; }
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        [Display(Name = "Leave Period")]
        public string LeavePeriod { get; set; }
    }
}
