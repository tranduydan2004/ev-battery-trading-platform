using Identity.Domain.Abtractions;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIndentityInfrastructure(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<AppDbContext>(o => o.UseNpgsql(connectionString));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileStorage, LocalFileStorage>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
