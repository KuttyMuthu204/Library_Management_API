using System.ComponentModel.DataAnnotations;

namespace Library_Management.ValidationAttributes
{
    /// <summary>
    /// Validation attribute that ensures a DateTime value is not in the future.
    /// </summary>
    public class NotInFutureAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date > DateTime.Today) 
                {
                    return new ValidationResult("Date cannot be in the future.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
