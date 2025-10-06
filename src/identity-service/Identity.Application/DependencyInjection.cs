using Identity.Application.Contracts;
using Identity.Application.Services;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityApplication (this IServiceCollection services)
        {
            services.AddScoped<IUserCommands, UserCommands>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
