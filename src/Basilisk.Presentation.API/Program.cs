using Basilisk.DataAccess;
using Basilisk.Business.Repositories;
using Basilisk.Business.Interfaces;
using Basilisk.Presentation.API.Suppliers;
using Basilisk.Presentation.API.Helpers;
using Microsoft.OpenApi.Models;
using Basilisk.Presentation.API.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

namespace Basilisk.Presentation.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.AddConsole();

        Dependencies.ConfigureServices(builder.Configuration, builder.Services);

        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<SupplierService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddControllers(
            // options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
            );
        // berguna untuk, apabila ada property yang harus atau wajib disi,tpi kita tidak mau mengisinya, sehinggan nilainya
        // akan bernilai null

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                               builder.Configuration.GetSection("AppSettings:TokenSignature").Value))
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(
                options =>
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Standard auth header using the bearer scheme",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    options.OperationFilter<SecurityRequirementsOperationFilter>(); //dotnet add package Swashbuckle.AspNetCore.Filters;
                }
            );


        var allowedSpecificOrigin = "_webBasililsk";

        builder.Services.AddCors(options
            => options
            .AddPolicy(name: allowedSpecificOrigin,
                       policy => policy.WithOrigins("http://localhost:5200")
                                       .AllowAnyMethod()
                                       .AllowAnyHeader())
            );

        builder.Services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Basilisk API" })
            );

        var app = builder.Build();

        app.UseCors(allowedSpecificOrigin);

        app.UseMiddleware<ErrorHandleMiddleware>();

        app.MapControllers();

        app.UseSwagger();
        app.UseSwaggerUI(
            configuration => configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Basilisk API V1")
        );

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}
