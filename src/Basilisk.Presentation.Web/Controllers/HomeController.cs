using Microsoft.AspNetCore.Mvc;

namespace Basilisk.Presentation.Web.Controllers;

[Route("Home")]
public class HomeController : Controller
{
    [HttpGet("DashBoard")]
    public IActionResult DashBoard()
    {
        return View();
    }
}

