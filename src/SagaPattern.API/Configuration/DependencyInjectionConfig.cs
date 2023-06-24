using Microsoft.EntityFrameworkCore;
using SagaPattern.API.Application.Services;
using SagaPattern.API.Data;

namespace SagaPattern.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("CatalogDB"));

        services.AddTransient<ProductPopulateService>();

        return services;
    }
}
