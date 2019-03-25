using Data;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddTransient<DbInitializer, DbInitializer>();

            return services;
        }
    }
}
