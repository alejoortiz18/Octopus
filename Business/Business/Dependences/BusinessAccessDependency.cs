using Business.AuthBusiness;
using Business.Interfaces;
using Business.UsuarioBll;
using Data.Interfaces;
using Data.Repository;
using Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using perfil = Business.AuthBusiness;
using Bancos =  Business.BancosBusiness;

namespace Business.Dependences
{
    public static class BusinessAccessDependency
    {
        public static IServiceCollection BusinessDependencyInjectionAccess(this IServiceCollection services)
        {

            #region [ Repository Data Access ]

            services.AddScoped<IAuthBusiness, perfil.AuthBusiness>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBancosRepository, BancoRepository>();
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            services.AddScoped<IBancosBusiness, Bancos.BancosBusiness>();
            // services.AddScoped<IUsuarioXSesionBusiness, UsuarioXSesionBusiness>();
            // services.AddScoped<ITipoEntrenamientoBusiness, TipoEntrenamientoBusiness>();
            // services.AddScoped<IPerfilBusiness, perfil.PerfilBusiness>();
            //
            #endregion

            return services;
        }
    }
}
