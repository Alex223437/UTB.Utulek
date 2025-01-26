using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UTB.Utulek.Infrastructure.Validation
{
    public class NumericOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string strValue)
            {
                if (!Regex.IsMatch(strValue, @"^\d+$"))
                {
                    return new ValidationResult("The field must contain only numeric characters.");
                }
            }

            return ValidationResult.Success;
        }
    }
}