using Basilisk.Presentation.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.ViewModels;
public class AccountChangeViewModel
{
    public string? Username { get; set; }

    public string? OldPassword { get; set; }

    [ChangePassword]
    public string? NewPassword { get; set; }

    public string? ConfirmNewPassword { get; set;}
}

