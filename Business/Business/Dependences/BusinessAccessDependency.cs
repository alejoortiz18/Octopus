
using Business.Interfaces;
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
            
            //services.AddScoped<ISesionBusiness, SesionBusiness>();
            //services.AddScoped<ISerieBusiness, SerieBusiness>();
            //services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            //services.AddScoped<INadadorXEntrenadorBusiness, NadadorXEntrenadorBusiness>();
            //services.AddScoped<IUsuarioXSesionBusiness, UsuarioXSesionBusiness>();
            //services.AddScoped<ITipoEntrenamientoBusiness, TipoEntrenamientoBusiness>();
            //services.AddScoped<IPerfilBusiness, perfil.PerfilBusiness>();
            //
            #endregion

            return services;
        }
    }
}
