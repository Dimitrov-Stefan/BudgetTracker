using Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Data;
using Data.Repositories;
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
            services.AddScoped<IFinancialItemsService, FinancialItemsService>();
            services.AddScoped<IFinancialOperationsService, FinancialOperationService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection RegisterAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFinancialItemsRepository, FinancialItemsRepository>();
            services.AddScoped<IFinancialOperationsRepository, FinancialOperationsRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
