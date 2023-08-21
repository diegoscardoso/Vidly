using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Vidly.Models.CustomValidations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomerBirthdayValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer?.MembershipTypeId == MembershipType.Unknow || 
                customer?.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer?.BirthDate == null)
                return new ValidationResult("Birthday is required.");

            return (DateTime.Now.Year - customer?.BirthDate.Value.Year >= 18)
                ? ValidationResult.Success : 
                new ValidationResult("Customer should be at least 18 years old.");
        }
    }
}
