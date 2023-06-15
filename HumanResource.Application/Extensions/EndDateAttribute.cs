using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Extensions
{
    public class EndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value;
            if (date > DateTime.Now)
            {
                return new ValidationResult("Selected date must be less than today.");
            }

            return ValidationResult.Success;
        }
    }
}
