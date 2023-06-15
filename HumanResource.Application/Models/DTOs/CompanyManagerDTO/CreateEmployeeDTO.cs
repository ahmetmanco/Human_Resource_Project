using HumanResource.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.DTOs.CompanyManagerDTO
{
    public class CreateEmployeeDTO
    {
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
        [Required(ErrorMessage = "Please select a country.")]
        public int? CountryId { get; set; }

        [Display(Name = ("City"))]
        [Required(ErrorMessage = "Please select a city.")]
        public int? CityId { get; set; }

        [Display(Name = ("District"))]
        public int? DistrictId { get; set; }

        [Display(Name = ("Address Description"))]
        [Required(ErrorMessage = "Address cannot be null.")]
        public string? AddressDescription { get; set; }

        [Display(Name = "Blood Type")]
        [Required(ErrorMessage = "Please select a blood type.")]
        public int? BloodTypeId { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please select a department.")]
        public int? DepartmentId { get; set; }

        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "Birth date cannot be null")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Recruitment Date")]
        [Required(ErrorMessage = "Recruitment date cannor be null")]
        public DateTime? RecruitmentDate { get; set; }

        [Display(Name = "Manager")]
        [Required(ErrorMessage = "Please select a manager.")]
        public Guid? ManagerId { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string? ImagePath { get; set; }


        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please select a title.")]
        public int? TitleId { get; set; }
        [ValidateNever]
        public int CompanyId { get; set; }

        [Display(Name = "Company Position")]
        public bool IsEmployee { get; set; }

        public DateTime CreatedDate => DateTime.Now;
        public int StatuId => Status.Active.GetHashCode();

    }
}
