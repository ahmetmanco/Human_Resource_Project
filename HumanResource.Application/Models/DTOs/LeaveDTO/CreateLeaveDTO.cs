using HumanResource.Application.Extensions;
using HumanResource.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.DTOs.LeaveDTO
{
    public class CreateLeaveDTO
    {

        public Guid UserId { get; set; }
        public DateTime CreatedDate => DateTime.Now;

        [Required(ErrorMessage = "Leave start date cannot be null.")]
        [Display(Name = "Leave Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Leave end date cannot be null.")]
        [Display(Name = "Leave End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Return date cannot be null.")]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Leave period cannot be null.")]
        [Display(Name = "Leave Period")]
        [Range(0, 731.99, ErrorMessage = "Please enter the total number of days off!")]
        public string LeavePeriod { get; set; }

        [Required(ErrorMessage = "Leave Type cannot be null.")]
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
        [ValidateNever]
        public int StatuId{ get; set; }
    }
}
