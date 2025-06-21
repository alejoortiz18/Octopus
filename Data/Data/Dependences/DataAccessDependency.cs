using Data.Interfaces;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Data.Dependences
{

    public static class DataAccessDependency
    {
        public static IServiceCollection DataDependencyInjectionAccess(this IServiceCollection services)
        {

            #region [ Repository Data Access ]

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBancosRepository, BancoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //services.AddScoped<ISeriesRepository, SeriesRepository>();
            //services.AddScoped<INadadorXEntrenadorRepository, NadadorXEntrenadorRepository>();
            //services.AddScoped<IUsuarioXSesionRepository, UsuarioXSesionRepository>();
            //services.AddScoped<ITipoEntrenamientoRepository, TipoEntrenamientoRepository>();
            //services.AddScoped<IPerfilRepository, PerfilRepository>();
            //
            #endregion


            #region [General]
            services.AddScoped<AppDbContext>();
            #endregion

            return services;
        }
    }

}
