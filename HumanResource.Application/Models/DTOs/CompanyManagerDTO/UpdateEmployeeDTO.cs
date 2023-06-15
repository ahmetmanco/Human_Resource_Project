using HumanResource.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.DTOs.CompanyManagerDTO
{
    public class UpdateEmployeeDTO
    {
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(30, ErrorMessage = "First name must be less than 30 characters.")]
        [Required(ErrorMessage = "First name cannot be null.")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Last name must be less than 50 characters.")]
        [Required(ErrorMessage = "Last name cannot be null.")]
        public string LastName { get; set; }


        [MinLength(6, ErrorMessage = "User name must be more than 6 characters.")]
        [MaxLength(30, ErrorMessage = "User name must be less than 30 characters.")]
        [Required(ErrorMessage = "User name cannot be null.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "E-mail cannot be null.")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone cannot be null.")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = ("Country"))]
        public int? CountryId { get; set; }

        [Display(Name = ("City"))]
        public int? CityId { get; set; }

        [Display(Name = ("District"))]
        public int? DistrictId { get; set; }

        [Display(Name = ("Address Description"))]
        public string? AddressDescription { get; set; }

        [Display(Name = "Blood Type")]
        public int? BloodTypeId { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Birt Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Recruitment Date")]
        public DateTime? RecruitmentDate { get; set; }

        [Display(Name = "Manager")]
        public Guid? ManagerId { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string? ImagePath { get; set; }


        [Display(Name = "Title")]
        public int? TitleId { get; set; }
        [Display(Name = "Company Position")]
        public bool IsEmployee { get; set; }


        public DateTime ModifiedDate => DateTime.Now;
        public int StatuId => Status.Active.GetHashCode();

        [ValidateNever]
        public string? BaseUrl { get; set; }
    }
}
