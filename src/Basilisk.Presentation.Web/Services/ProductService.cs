using Basilisk.Business.Interfaces;
using Basilisk.Business.Repositories;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basilisk.Presentation.Web.Service;
public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public ProductsIndexViewModel GetAllProducts(int pageNumber, int pageSize, string? name, long? supplierID, long? categoryID)
    {
        List<ProductViewModel> result;

        result = _productRepository.GetAllProduct(pageNumber, pageSize, name, supplierID, categoryID)
                .Select(c => new ProductViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SupplierId = c.SupplierId,
                        CategoryId = c.CategoryId,
                        Description = c.Description,
                        Price = c.Price,
                        Stock = c.Stock,
                        OnOrder = c.OnOrder,
                        Discontinue = c.Discontinue,
                        Supplier = new Supplier
                        {
                            CompanyName = c.Supplier.CompanyName
                        },
                        Category = new Category
                        {
                            Name = c.Category.Name,
                        }
                   
                    })
                 .ToList();

        int totalCategories = _productRepository.CountProduct(name);


        return new ProductsIndexViewModel
        {
            Products = result,
            Name = name,
            pagenationInfoViewModel = new PaginationInfoViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalCategories
            },
            Categories = GetCategories(),
            Suppliers = GetSupplier()

        };
    }

    public ProductViewModel GetByID(long id)
    {

        var result = _productRepository.GetProductByID(id);

        var resultHasil = new ProductViewModel()
        {
            Id= result.Id,
            Name = result.Name,
            SupplierId = result.SupplierId,
            CategoryId = result.CategoryId,
            Description = result.Description,
            Price = result.Price,
            Stock = result.Stock,
            OnOrder = result.OnOrder,
            Discontinue = result.Discontinue,
            Categories = GetCategories(),
            Suppliers = GetSupplier(),
            Supplier = new Supplier
            {
                CompanyName = result.Supplier.CompanyName
            },
            Category = new Category
            {
                Name = result.Category.Name
            }
        };


        return resultHasil;
    }

    public void InsertProduct(ProductViewModel productView)
    {

        Product result = new Product()
        {
            Id = productView.Id,
            Name = productView.Name,
            SupplierId = productView.SupplierId,
            CategoryId = productView.CategoryId.Value,
            Description = productView.Description,
            Price = productView.Price,
            Stock = productView.Stock,
            OnOrder = productView.OnOrder,
            Discontinue = productView.Discontinue
        };

        _productRepository.InsertProduct(result);
    }
    public void UpdateProduct(ProductViewModel productView)
    {
        Product result = new Product()
        {
            Id = productView.Id,
            Name = productView.Name,
            SupplierId= productView.SupplierId,
            CategoryId = productView.CategoryId.Value,
            Description = productView.Description,
            Price = productView.Price,
            Stock = productView.Stock,
            OnOrder = productView.OnOrder,
            Discontinue = productView.Discontinue
        };

        _productRepository.UpdateProduct(result);
    }

/*    public void UpdateCategory(long id)
    {
        Category result = new Category()
        {
            Id = categoryView.Id,
            Description = categoryView.Description,
            Name = categoryView.Name
        };

        _categoryRepository.UpdateCategory(result);
    }*/
    public void DeleteProduct(long id)
    {
        _productRepository.DeleteProduct(id);
    }

    public List<SelectListItem> GetCategories()
    {
        var category = _categoryRepository.GetAllCategories();

        var selectListItem = category.OrderBy(c => c.Name)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            })
            .ToList();


        return selectListItem;
    }

    public List<SelectListItem> GetSupplier()
    {
        var supplier = _productRepository.GetAllSupplier();

        var selectListItem = supplier.OrderBy(c => c.CompanyName)
            .Where(c => c.DeleteDate == null)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CompanyName
            })
            .ToList();

        return selectListItem;
    }

    public ProductDetailsViewModel GetByIDDetail(long id)
    {

        var result = _productRepository.GetProductByID(id);


        var resultHasil = new ProductViewModel()
        {
            Id = result.Id,
            Name = result.Name,
            SupplierId = result.SupplierId,
            CategoryId = result.CategoryId,
            Description = result.Description,
            Price = result.Price,
            Stock = result.Stock,
            OnOrder = result.OnOrder,
            Discontinue = result.Discontinue,
            Categories = GetCategories(),
            Suppliers = GetSupplier(),
            Supplier = new Supplier
            {
                CompanyName = result.Supplier.CompanyName
            },
            Category = new Category
            {
                Name = result.Category.Name
            }
        };


        return new ProductDetailsViewModel
        {
            ProductViewModel = resultHasil
        };
    }

    public ProductDetailsViewModel GetMonthlyProduct(long Id)
    {
        var result = _productRepository.GetProductMonthly(Id);

        var resultHasil = result
            .Select(c => new MonthlyReportViewModel
            {
                Year = c.Year,
                Month = c.Month,
                Sold = c.sold,
                Total = c.Total
            }).ToList();

        return new ProductDetailsViewModel
        {
            Id = Id,
            MonthlyReports = resultHasil
        };

    }

    public ProductDetailsViewModel GetYearlyProduct(long Id)
    {
        var result = _productRepository.GetProductYearly(Id);

        var resultHasil = result
            .Select(c => new YearlyReportViewModel
            {
                Year = c.Year,
                Sold = c.sold,
                Total = c.Total
            }).ToList();

        return new ProductDetailsViewModel
        {
         Id = Id,   
            YearlyReports = resultHasil
        };

    }
}

