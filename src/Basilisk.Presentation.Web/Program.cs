using Basilisk.DataAccess;
using Basilisk.Presentation.Web.Configuration;
using Basilisk.Presentation.Web.Service;
using Basilisk.Presentation.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Basilisk.Presentation.Web;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Menambahkan fungsi MVC kedalam fungsinya

        IServiceCollection services = builder.Services;

        Dependencies.ConfigureServices(builder.Configuration,services);

        services.AddBussinesServices();

        services.AddScoped<CategoryService>();
        services.AddScoped<ProductService>();
        services.AddScoped<AuthService>();



        services.AddControllersWithViews();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.AccessDeniedPath = "/auth/AccessDenied";
                });


        var app = builder.Build();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        /*app.MapGet("/", () => "Hello World!");*/
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action}"
            );

        app.Run();
    }
}
