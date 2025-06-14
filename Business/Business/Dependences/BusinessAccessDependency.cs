using Business.AuthBusiness;
using Business.Interfaces;
using Business.UsuarioBll;
using Data.Interfaces;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using perfil = Business.AuthBusiness;


namespace Business.Dependences
{
    public static class BusinessAccessDependency
    {
        public static IServiceCollection BusinessDependencyInjectionAccess(this IServiceCollection services)
        {

            #region [ Repository Data Access ]

            services.AddScoped<IAuthBusiness, perfil.AuthBusiness>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            // services.AddScoped<ISerieBusiness, SerieBusiness>();
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            // services.AddScoped<INadadorXEntrenadorBusiness, NadadorXEntrenadorBusiness>();
            // services.AddScoped<IUsuarioXSesionBusiness, UsuarioXSesionBusiness>();
            // services.AddScoped<ITipoEntrenamientoBusiness, TipoEntrenamientoBusiness>();
            // services.AddScoped<IPerfilBusiness, perfil.PerfilBusiness>();
            //
            #endregion

            return services;
        }
    }
}
