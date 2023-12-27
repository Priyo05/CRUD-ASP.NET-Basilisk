using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.DataAccess;
public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration,IServiceCollection services)
    {
        services.AddDbContext<BasiliskTfContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BasiliskConnection"))
        );
    }

}

