using Demo.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
namespace Demo.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

    }
}
