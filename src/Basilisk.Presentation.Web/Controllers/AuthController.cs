using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Basilisk.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Basilisk.Presentation.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Basilisk.Presentation.Web.Services;
using Basilisk.Presentation.Web.ViewModels.Enum;

namespace Basilisk.Presentation.Web.Controllers;

[Route("Auth")]
public class AuthController : Controller
{

    private readonly AuthService authService;

    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View(new UserRegisterViewModel
        {
            Roles = authService.GetRoles(),
        });
    }

    [HttpPost("Register")]
    public IActionResult Register(UserRegisterViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(new UserRegisterViewModel
            {
                Roles = authService.GetRoles(),
            });
        }
        authService.Register(vm);
        return RedirectToAction("Login");
    }


    [HttpPost("login")]
    //penambahan async Task<IactionResult> untk yang memiliki return
    public async Task<IActionResult> Login(UserLoginViewModel vm)
    {
        try
        {
            var authTicket = authService.Login(vm);
            //untuk menggunakan operasi asynchronus maka
            // await ini pada methodnya perlu penambahan seperti diatas
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                authTicket.Principal, authTicket.Properties);

            return RedirectToAction("DashBoard", "Home");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ViewBag.ErrorMessage = e.Message;
            return View(vm);
        }
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }

    [Authorize]
    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

}

