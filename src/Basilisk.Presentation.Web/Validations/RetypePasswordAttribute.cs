using Basilisk.Presentation.Web.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validations;
[AttributeUsage(AttributeTargets.Property)]
public class RetypePasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
            var reTypePassword = (UserRegisterViewModel)validationContext.ObjectInstance; // dirinya sendiri

            //object reflection,tidak usah di pake 
            /*            var discontinue = product.GetType().GetProperty("Discontinue");
                        var isDiscontinued = discontinue.GetValue(unknownObject);*/

            if (reTypePassword.RetypePassword == reTypePassword.Password)
            {
                return ValidationResult.Success;
            }

            else
            {
                return new ValidationResult("Password not match");
            }
        
    }
}

