using Basilisk.Business.Repositories;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.Service;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;

[Authorize(Roles = "Administrator,Finance,Salesman")]
[Route("Product")]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;

    private readonly ProductService _services;

    //Depedency injection untuk si ILogger
    public ProductController(ILogger<ProductController> logger, ProductService productService)
    {
        _logger = logger;
        _services = productService;
    }

    [Authorize(Roles ="Administrator,Finance,Salesman")]
    [HttpGet("Index")]
    public IActionResult Index(string? name, long? supplierID, long? categoryID, int pageSize = 7, int pageNumber = 1)
    {

        var vm = _services.GetAllProducts(pageNumber, pageSize, name, supplierID, categoryID);
        return View(vm);
    }
    [Authorize(Roles = "Administrator")]
    [HttpGet("Insert")]
    public IActionResult Insert()
    {
        return View("Insert", new ProductViewModel
        {
            Suppliers = _services.GetSupplier(),
            Categories = _services.GetCategories()

        });
    }

    [HttpPost("Insert")]
    public IActionResult Insert(ProductViewModel vm)
    {
        // Modelstate untuk validasi
        if (!ModelState.IsValid)
        {
            vm.Suppliers = _services.GetSupplier();
            vm.Categories = _services.GetCategories();
            return View("Insert", vm);
        }
        _services.InsertProduct(vm);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("Update/{id}")]
    public IActionResult Update(long id)
    {
        ProductViewModel vm = _services.GetByID(id);
        return View("Update", vm);
    }

    [HttpPost("Update/{id}")]
    public IActionResult Update(ProductViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Suppliers = _services.GetSupplier();
            vm.Categories = _services.GetCategories();
            return View("Update", vm);
        }
        _services.UpdateProduct(vm);
        return RedirectToAction("Index");
    }


    /*    [HttpGet("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            _services.DeleteProduct(id);

            return RedirectToAction("Index");

        }*/
    [Authorize(Roles = "Administrator")]
    [HttpGet("Detail/{id}")]
    public IActionResult Detail(long id)
    {
        ProductDetailsViewModel vm = _services.GetByIDDetail(id);
        return View("Detail", vm);
    }
    [Authorize(Roles = "Administrator")]
    [HttpGet("Detail/MonthlyReport/{id}")]
    public IActionResult MonthlyReport(long id)
    {
        ProductDetailsViewModel vm = _services.GetMonthlyProduct(id);
        return View("MonthlyReport", vm);
    }
    [Authorize(Roles = "Administrator")]
    [HttpGet("Detail/YearlyReport/{id}")]
    public IActionResult YearlyReport(long id)
    {
        ProductDetailsViewModel vm = _services.GetYearlyProduct(id);
        return View("YearlyReport", vm);
    }



}

