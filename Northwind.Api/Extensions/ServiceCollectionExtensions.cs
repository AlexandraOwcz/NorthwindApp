using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Northwind.Api.Models;
using Northwind.Api.Repository;
using Northwind.Api.UoW;
using Northwind.Data.Entities;

namespace Northwind.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NorthwindDatabaseSettings>(
                configuration.GetSection(nameof(NorthwindDatabaseSettings)));

            services.AddSingleton<INorthwindDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<NorthwindDatabaseSettings>>().Value);            

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<INorthwindMongoDbContext, NorthwindContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}