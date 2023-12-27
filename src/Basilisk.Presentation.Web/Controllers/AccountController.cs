using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.Security.Claims;

namespace Basilisk.Presentation.Web.Controllers;
[Authorize(Roles = "Administrator,Finance,Salesman")]
[Route("Account")]
public class AccountController : Controller
{
    private AuthService _authService;

    public AccountController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("Change")]
    public IActionResult Change()
    {
        return View();
    }

    [HttpPost("Change")]
    public IActionResult Change(AccountChangeViewModel vm)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                return View("Change", vm);
            }
            _authService.ChangePassword(vm);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View("Change", vm);
        }
        return RedirectToAction("Dashboard", "Home");
    }

}

