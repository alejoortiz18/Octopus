using Business.Interfaces;
using Data.Interfaces;
using Models.Dto.Auth;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AuthBusiness
{
    public class AuthBusiness : IAuthBusiness
    {
        //private readonly IUsuarioRepository _usuarioRepository;
       // private readonly IEmailHelper _emailHelper;
        private readonly IAuthRepository _authRepository;

        public AuthBusiness(/*IUsuarioRepository usuarioRepository,*/ /*IEmailHelper emailHelper,*/ IAuthRepository authRepository)
        {
            //_usuarioRepository = usuarioRepository;
            //_emailHelper = emailHelper;
            _authRepository = authRepository;
        }

        public async Task<bool> GenerarCodigoRestablecimientoAsync(string email)
        {
            //var usuario = _usuarioRepository.ObtenerPorEmail(email);
            //if (usuario != null)
            //{
            //    usuario.CodigoRestablecerPassword = Guid.NewGuid().ToString("N").Substring(0, 6);
            //    usuario.CodigoActivo = true;
            //    await _usuarioRepository.ActualizarUsuarioAsync(usuario);

            //    await _emailHelper.EnviarCorreoAsync(usuario.Correo, usuario.CodigoRestablecerPassword, Email.AsuntoRestablecerContrasena, Email.CuerpoRestablecerContrasena);
            //    return true;
            //}
            return false;
        }

        public async Task<Usuario> InicioSesionAsync(LoginDto user)
        {
            Usuario? usuario = await _authRepository.LoginAsync(user);

            if (usuario != null)
            {
                return usuario;
            }
            return null;
        }

        //public bool ChangePassword(UserCod user)
        //{
        //    bool resultValid = _authRepository.CodigoIsValid(user);
        //    if (resultValid)
        //    {
        //        return _authRepository.ChangePassword(user);

        //    }
        //    return false;
        //}
    }
}
