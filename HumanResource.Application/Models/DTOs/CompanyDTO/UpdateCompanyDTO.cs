using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.DTOs.CompanyDTO
{
    public class UpdateCompanyDTO
    {
        [Display(Name = "Company Id")]
        public int CompanyId { get; set; }

        [MinLength(3, ErrorMessage = "Company name must be more than 3 characters.")]
        [Required(ErrorMessage = "Company name cannot be null.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Tax Number cannot be null.")]
        [Display(Name = "Tax Number")]
        public string TaxNumber { get; set; }

        [MinLength(3, ErrorMessage = "TaxOffice name must be more than 3 characters.")]
        [MaxLength(50, ErrorMessage = "TaxOffice name must be less than 50 characters.")]
        [Required(ErrorMessage = "TaxOffice name cannot be null.")]
        [Display(Name = "TaxOffice Name")]
        public string TaxOfficeName { get; set; }

        [Required(ErrorMessage = "Phone cannot be null.")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Number Of Employee cannot be null.")]
        [Display(Name = "Number Of Employee")]
        public string NumberOfEmployee { get; set; }
        public DateTime ModifiedDate => DateTime.Now;
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        [Display(Name = ("City"))]
        public int? CityId { get; set; }

        [Display(Name = ("District"))]
        public int? DistrictId { get; set; }

        [Display(Name = ("Address Description"))]
        public string? AddressDescription { get; set; }

        [Display(Name = "Manager")]
        public Guid? ManagerId { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string? ImagePath { get; set; }

        [ValidateNever]
        public Statu Statu { get; set; }
        public int StatuId => Status.Active.GetHashCode();
        


    }
}
