using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class ChangePasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var result = (AccountChangeViewModel)validationContext.ObjectInstance;

        if (result.OldPassword == result.NewPassword)
        {
            return new ValidationResult("New password cannot be the same as your current password");
        } else if (result.NewPassword != result.ConfirmNewPassword)
        {
            return new ValidationResult("new password does not match with confirm password");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}

