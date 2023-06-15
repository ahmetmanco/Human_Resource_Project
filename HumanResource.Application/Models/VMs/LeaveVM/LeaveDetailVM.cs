using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.VMs.LeaveVM
{
    public class LeaveDetailVM
    {
        public int Id { get; set; }
        [Display(Name = "Personel Name")]
        public string PersonelName { get; set; }
        [Display(Name = "Created Date")]
        public string CreatedDate { get; set; }
        [Display(Name ="Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        [Display(Name = "Return Date")]
        public string ReturnDate { get; set; }
        [Display(Name = "Leave Period")]
        public string LeavePeriod { get; set; }
        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }
        [Display(Name ="Description")]
        public string Description { get; set; }
        [ValidateNever]
        public string Statu { get; set; }

    }
}
