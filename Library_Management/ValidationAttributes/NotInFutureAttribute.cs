using System.ComponentModel.DataAnnotations;

namespace Library_Management.ValidationAttributes
{
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
