using Microsoft.Extensions.DependencyInjection;
using Data.Dependences;
using Business.Dependences;
using Helpers.Dependences;

namespace Octopus.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection DependencyInjection(this IServiceCollection services)
        {
            services.DataDependencyInjectionAccess();
            services.HelperDependencyInjectionAccess();
            services.BusinessDependencyInjectionAccess();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .WithOrigins("*"));
            });

            return services;
        }
    }
}
