using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.ViewModels;
public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Supplier")]
    public long? SupplierId { get; set; }
    [Display(Name = "Category")]
    [Required(ErrorMessage ="Must have a Category, please choose one!")]
    public long? CategoryId { get; set; }
    [Range(minimum: 0,maximum: int.MaxValue,ErrorMessage ="Cannot be a negative number")]
/*    [StocksOnOrder]*/
    public int Stock { get; set; }
    public bool Discontinue { get; set; } = true;
    [Range(minimum: 0, maximum: double.MaxValue, ErrorMessage = "Cannot be a negative number")]
    public decimal Price { get; set; }
    [Display(Name = "On Order")]
    [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Cannot be a negative number")]
    [Reorderable]
    public int OnOrder { get; set; }
    public List<SelectListItem>? Suppliers { get; set;}
    public List<SelectListItem>? Categories { get; set; }
    public Supplier? Supplier { get; set; }
    public Category? Category { get; set; }

/*    public SupplierViewModel? Supplier { get; set; }
    public CategoryViewModel? Category { get; set; }*/


}

