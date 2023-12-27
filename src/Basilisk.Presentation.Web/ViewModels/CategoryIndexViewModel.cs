namespace Basilisk.Presentation.Web.ViewModels;
public class CategoryIndexViewModel
{
    public List<CategoryViewModel> Categories { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public PaginationInfoViewModel pagenationInfoViewModel { get; set; }
    public List<ProductInfoViewModel> Products { get; set; }

}

