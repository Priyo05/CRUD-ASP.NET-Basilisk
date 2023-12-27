using Basilisk.Business.Interfaces;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.Helpers;
using Basilisk.Presentation.Web.ViewModels;
using Basilisk.Presentation.Web.ViewModels.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Basilisk.Presentation.Web.Services;
public class AuthService
{
    private readonly IAuthRepository _accountRepository;

    public AuthService(IAuthRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Register(UserRegisterViewModel vm)
    {

        Account account = new Account
        {
            Username = vm.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
            Role = vm.Role,
        };
        _accountRepository.RegisterAccount(account);

    }

    public AuthenticationTicket Login(UserLoginViewModel vm)
    {
        var user = _accountRepository.GetAccount(vm.Username);

        //var user = _Context.Accounts.FirstOrDefault(u => u.Username.Equals(vm.Username))
        //    ?? throw new KeyNotFoundException("Username salah atau belum terdaftar");

        //memverifikasi apakah password yang diinput(VM) benar dan cocok dengan yang ada di user
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(vm.Password, user.Password);

        if (!isCorrectPassword)
        {
            throw new PasswordException("Username atau Password anda salah");
        }

        //principal
        ClaimsPrincipal principal = GetPrincipal(user);


        AuthenticationTicket authenticationTicket = GetAuthenticationTicket(principal);


        return authenticationTicket;
    }
    public ClaimsPrincipal GetPrincipal(Account user)
    {
        var claim = new List<Claim>
        {
            new Claim("username", user.Username),
            new Claim(ClaimTypes.Role,user.Role.ToString())
        };

        ClaimsIdentity identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

        return new ClaimsPrincipal(identity);
    }

    public AuthenticationTicket GetAuthenticationTicket(ClaimsPrincipal principal)
    {
        AuthenticationProperties authenticationProperties = new AuthenticationProperties
        {
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(20),
            AllowRefresh = false
        };

        AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties,
                                                            CookieAuthenticationDefaults.AuthenticationScheme);

        return authenticationTicket;
    }
    public List<SelectListItem> GetRoles()
    {
        var roles = Enum.GetValues(typeof(Role));

        List<SelectListItem> result = new List<SelectListItem>();

        foreach (Role role in roles)
        {
            result.Add(new SelectListItem
            {
                Text = role.GetLabel(),
                Value = role.ToString()
            });
        }

        return result;
    }

    public void ChangePassword(AccountChangeViewModel vm)
    {
        var users = _accountRepository.GetAllAccount();

        Account user = users.FirstOrDefault(user => user.Username.Equals(vm.Username))
            ?? throw new KeyNotFoundException("Username salah");


        bool isMatch = BCrypt.Net.BCrypt.Verify(vm.OldPassword, user.Password);
        if (!isMatch)
        {
            throw new PasswordException("Password anda salah");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(vm.NewPassword);

        _accountRepository.ChangePassword(user);
    }

}

