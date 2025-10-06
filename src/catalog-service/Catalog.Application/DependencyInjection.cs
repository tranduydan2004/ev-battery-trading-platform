using Catalog.Application.Contracts;
using Catalog.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductQueries, ProductQueries>();
            services.AddScoped<IProductCommands, ProductCommands>();
            return services;
        }
    }
}
