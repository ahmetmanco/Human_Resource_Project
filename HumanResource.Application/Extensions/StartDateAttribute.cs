using System.ComponentModel.DataAnnotations;


namespace HumanResource.Application.Extensions
{
    public class StartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value;
            if(date < DateTime.Now)
            {
                return new ValidationResult("Selected date must be greater than today.");
            }

            return ValidationResult.Success;
        }
    }
}
