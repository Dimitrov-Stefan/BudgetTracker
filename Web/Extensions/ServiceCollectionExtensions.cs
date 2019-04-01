using Data;
using Microsoft.Extensions.DependencyInjection;
using Web.Services;

namespace Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddTransient<DbInitializer, DbInitializer>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
