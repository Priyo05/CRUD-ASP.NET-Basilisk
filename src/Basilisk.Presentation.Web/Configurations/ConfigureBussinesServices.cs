using Basilisk.Business.Repositories;
using Basilisk.Business.Interfaces;

namespace Basilisk.Presentation.Web.Configuration;
public static class ConfigureBussinesServices
{
    
    public static IServiceCollection AddBussinesServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        return services;
    }


}

