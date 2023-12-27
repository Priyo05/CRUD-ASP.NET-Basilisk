using Basilisk.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class UniqueCategoryNameAttribute : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
/*        if (value == null)
        {
            return ValidationResult.Success;
        }*/

        var categoryName = (string)value;
        var dbContext = (BasiliskTfContext)validationContext.GetService(typeof(BasiliskTfContext));

        var nameIsExist = dbContext.Categories.Any(c => c.Name.Equals(categoryName));

        if (nameIsExist)
        {
            return new ValidationResult("Category name is already taken");
        }
        {
            return ValidationResult.Success;
        }
    }
}

