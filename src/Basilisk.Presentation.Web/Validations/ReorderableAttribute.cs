using Basilisk.Presentation.Web.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class ReorderableAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int onOrder)
        {
            var product = (ProductViewModel)validationContext.ObjectInstance; // dirinya sendiri

            //object reflection,tidak usah di pake 
/*            var discontinue = product.GetType().GetProperty("Discontinue");
            var isDiscontinued = discontinue.GetValue(unknownObject);*/

            if (product.Discontinue && onOrder > 0)
            {
                return new ValidationResult("You cant order when product is discontinue");
            }else
            {
                if (product.OnOrder > product.Stock)
                {
                    return new ValidationResult("You cannot order because stock is not enough");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

        }
        else
        {
            return new ValidationResult("This validation is only available for numbers");
        }
    }
}

