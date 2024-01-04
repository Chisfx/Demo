using Demo.Application.Interfaces.Contexts;
using Demo.Application.Interfaces.Repositories;
using Demo.Application.Interfaces.Shared;
using Demo.Infrastructure.DbContexts;
using Demo.Infrastructure.Repositories;
using Demo.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
namespace Demo.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICompareObject, CompareObjectService>();
        }
    }
}
