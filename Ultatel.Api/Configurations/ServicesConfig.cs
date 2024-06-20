using Ultatel.Core.Interfaces;
using Ultatel.Core.Profiles;
using Ultatel.Data.Repositories;
using Ultatel.Services.Services;

namespace Ultatel.Api.Configurations
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAuthService, AuthServicecs>();
            services.AddAutoMapper(typeof(UserProfile));

            return services;
        }
    }
}
