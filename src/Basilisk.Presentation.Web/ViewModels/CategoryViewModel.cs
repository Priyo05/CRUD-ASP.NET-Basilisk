using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.Validations;

namespace Basilisk.Presentation.Web.ViewModels;
public class CategoryViewModel
{
    public long Id { get; set; }
    [UniqueCategoryName]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

/*    public List<ProductViewModel>? Products { get; set; }*/

    public override string? ToString()
    {
        return $"ID: {Id} \n" +
            $"Name : {Name} \n" +
            $"Description : {Description}";
    }

}

