using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.ViewModels;
public class ProductsIndexViewModel
{
    public List<ProductViewModel> Products { get; set; }
    public string Name { get; set; }
    [Display(Name ="Supplier")]
    public long? SupplierId { get; set; }
    [Display(Name = "Category")]
    public long CategoryId { get; set; }
    public List<SelectListItem> Categories { get; set; }
    public List<SelectListItem> Suppliers { get; set; }
    public PaginationInfoViewModel pagenationInfoViewModel { get; set; }
    public List<ProductInfoViewModel> ProductsInfo { get; set; }

}

