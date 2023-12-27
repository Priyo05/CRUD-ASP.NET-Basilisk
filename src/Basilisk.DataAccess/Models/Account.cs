using Basilisk.Presentation.Web.ViewModels.Enum;
using System;
using System.Collections.Generic;

namespace Basilisk.DataAccess.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }
}
