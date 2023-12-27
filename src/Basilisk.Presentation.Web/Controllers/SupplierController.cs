using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;
[Authorize(Roles ="Administrator,Finance")]
[Route("Suppliers")]
public class SupplierController : Controller
{
    [HttpGet()]
    public IActionResult Index()
    {
        return View();
    }
}

