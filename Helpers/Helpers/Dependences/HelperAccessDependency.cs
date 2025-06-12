using Helpers.Codigo;
using Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Helpers.Dependences
{
    public static class HelperAccessDependency
    {
        public static IServiceCollection HelperDependencyInjectionAccess(this IServiceCollection services)
        {

            #region [ Repository Data Access ]

            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<ICodigoHelper, CodigoGeneradorHelperHelper>();
            //services.AddScoped<IPedidoRepository, PedidoRepository>();
            //services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
            //
            #endregion


            #region [General]
            services.AddScoped<AppDbContext>();
            #endregion

            return services;
        }
    }
}
