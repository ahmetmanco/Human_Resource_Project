using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Models.DTOs.AccountDTO
{
    public class RegisterDTO
    {
        [MinLength(6, ErrorMessage = "You cannot enter your Username less than 6 characters."), 
			Required(ErrorMessage = "User name cannot be null"), 
			Display(Name = "User Name")]
        public string UserName { get; set; }
        [StringLength(30, ErrorMessage = "Your name cannot exceed 30 characters"), Required(ErrorMessage = "First name cannot be null"), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Your lastname cannot exceed 50 characters"), Required(ErrorMessage = "Last name cannot be null"), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [MinLength(6, ErrorMessage = "You cannot enter your password less than 6 characters."), Required(ErrorMessage = "Password name cannot be null"), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Your passwords are not matched!"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [ Required(ErrorMessage = "Email cannot be null"), Display(Name = "E-Mail"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

		[MinLength(3, ErrorMessage = "Company name must be more than 3 characters.")]
		[Required(ErrorMessage = "Company name cannot be null.")]
		[Display(Name = "Company Name")]
		public string CompanyName { get; set; }

		[Required(ErrorMessage = "Tax Number cannot be null.")]
		[Display(Name = "Tax Number")]
		[RegularExpression("[0-9]{10}", ErrorMessage = "It must be 10 integers.")]
		public string TaxNumber { get; set; }

		[MinLength(3, ErrorMessage = "TaxOffice name must be more than 3 characters.")]
		[MaxLength(50, ErrorMessage = "TaxOffice name must be less than 50 characters.")]
		[Required(ErrorMessage = "TaxOffice name cannot be null.")]
		[Display(Name = "TaxOffice Name")]
		public string TaxOfficeName { get; set; }

		[Required(ErrorMessage = "Phone cannot be null.")]
		[Display(Name = "Phone Number")]
		[DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{11}", ErrorMessage = "Format must be 05** *** ** ** without space.")]
        public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Number Of Employee cannot be null.")]
		[Display(Name = "Number Of Employee")]
		public string NumberOfEmployee { get; set; }

        [Required(ErrorMessage = "Country cannot be null.")]
        [Display(Name = ("Country"))]
        public int? CountryId { get; set; }

        [Required(ErrorMessage = "City cannot be null.")]
		[Display(Name = ("City"))]
		public int? CityId { get; set; }

		[Required(ErrorMessage = "District cannot be null.")]
		[Display(Name = ("District"))]
		public int? DistrictId { get; set; }

		[Required(ErrorMessage = "Address Description cannot be null.")]
		[Display(Name = ("Address Description"))]
		public string? Description { get; set; }

        [Required(ErrorMessage = "Sector cannot be null.")]
        [Display(Name = ("Sector"))]
        public int CompanySectorId { get; set; }
        public DateTime CreatedDate => DateTime.Now;
	}
}
