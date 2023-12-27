namespace Basilisk.Presentation.Web.ViewModels.Enum;
public static class EnumExtension
{
    private static readonly Dictionary<Role, string> _roleEnumlabels = new Dictionary<Role, string>
    {
        {Role.Administrator, "Administrator"},
        {Role.Finance,"Finance"},
        {Role.Salesman,"Salesman" }
    };


    public static string GetLabel(this Role roleEnum)
    {
        return _roleEnumlabels[roleEnum];
    } 

    public static string Test()
    {
        return Role.Administrator.GetLabel();
    }
}

