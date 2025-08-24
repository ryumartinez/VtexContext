using Infrastructure.Abstractions;
using Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceInjection
{
    public static void ConfigureServices(IServiceCollection services)
    { 
        services.AddHttpClient();
        services.AddKeyedScoped<IProductRepository, VtexProductRepository>("Vtex");
        services.AddKeyedScoped<IProductRepository, SqlProductRepository>("Sql");
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}