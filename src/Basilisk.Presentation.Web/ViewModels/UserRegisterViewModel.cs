using Basilisk.Presentation.Web.Validations;
using Basilisk.Presentation.Web.ViewModels.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Basilisk.Presentation.Web.ViewModels;
public class UserRegisterViewModel
{
    [UniqueUserNameAttribute]
    public string Username { get; set; }
    public string Password { get; set; }
    [RetypePasswordAttribute]
    public string RetypePassword { get; set; }
    public Role Role { get; set; }
    public List<SelectListItem>? Roles { get; set; }
}

