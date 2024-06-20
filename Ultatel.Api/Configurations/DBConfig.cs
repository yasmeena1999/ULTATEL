using Microsoft.EntityFrameworkCore;
using Ultatel.Data.Data;

namespace Ultatel.Api.Configurations
{
    public static class DBConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                    throw new InvalidOperationException("Connection String is not found"));
            });

            return services;
        }
    }
}

