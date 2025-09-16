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
        services.AddHttpClient<Infrastructure.openapi_specClient>(client =>
        {
            // Set the base address from your configuration
            client.BaseAddress = new Uri("BaseUrl");

            // Add the authentication headers that are required for EVERY request
            client.DefaultRequestHeaders.Add("X-VTEX-API-AppKey", "AppKey");
            client.DefaultRequestHeaders.Add("X-VTEX-API-AppToken", "AppToken");
        });
        services.AddKeyedScoped<IProductRepository, VtexProductRepository>("Vtex");
        services.AddKeyedScoped<IProductRepository, SqlProductRepository>("Sql");
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}