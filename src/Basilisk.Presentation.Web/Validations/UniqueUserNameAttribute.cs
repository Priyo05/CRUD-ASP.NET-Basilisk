using Basilisk.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class UniqueUserNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userName = (string)value;
        var dbContext = (BasiliskTfContext)validationContext.GetService(typeof(BasiliskTfContext));

        var userNameExist = dbContext.Accounts.Any(a => a.Username.Equals(userName));

        if (userNameExist)
        {
            return new ValidationResult("Username is already taken");
        }
        {
            return ValidationResult.Success;
        }

    }
}

