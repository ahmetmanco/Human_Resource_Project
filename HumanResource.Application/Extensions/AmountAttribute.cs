using System.ComponentModel.DataAnnotations;

namespace HumanResource.Application.Extensions
{
    public class AmountAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Decimal amount = (Decimal)value;
            if (amount == 0)
            {
                return new ValidationResult("please enter a non-zero (0) value");
            }
            return ValidationResult.Success;
        }
    }
}
