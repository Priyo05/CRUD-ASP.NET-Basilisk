using Basilisk.Presentation.Web.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class StocksOnOrder : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int stock)
        {
            
            var product = (ProductViewModel)validationContext.ObjectInstance;

            if (product.OnOrder > product.Stock)
            {
                return new ValidationResult("You cannot order because stock is not enough");
            } else
            {
                return ValidationResult.Success;
            }

        }
        else
        {
            return new ValidationResult("This validation is only available for number");
        }
    }
}

