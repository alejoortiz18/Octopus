using Business.Interfaces;
using Constant;
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
        private readonly IUsuarioRepository _usuarioRepository;
       // private readonly IEmailHelper _emailHelper;
        private readonly IAuthRepository _authRepository;

        public AuthBusiness(IUsuarioRepository usuarioRepository, /*IEmailHelper emailHelper,*/ IAuthRepository authRepository)
        {
            _usuarioRepository = usuarioRepository;
            //_emailHelper = emailHelper;
            _authRepository = authRepository;
        }

        #region METODOS PARA LOGIN 
        
        public async Task<Usuario> InicioSesionAsync(LoginDto user)
        {
            Usuario? usuario = await _authRepository.LoginAsync(user);

            if (usuario != null)
            {
                return usuario;
            }
            return null;
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

        #endregion




        #region Métodos para SingUP, creación de usuario
        public async Task<(bool Success, string ErrorMessage)> SignUpAsync(SignUpDto signUpDto)
        {
            // Validar si el correo ya existe
            var existingUser = _usuarioRepository.ObtenerPorEmail(signUpDto.Email);
            if (existingUser != null)
            {
                return (false, UsuarioMsn.CorreoExiste);
            }

            // Generar Salt y Hash
            CrearHashConSalt(signUpDto.Password, out byte[] hash, out byte[] salt);

            // Crear entidad de usuario
            var usuario = new Usuario
            {
                NombreCompleto = signUpDto.Username,
                Email = signUpDto.Email,
                ContrasenaHash = hash,
                ContrasenaSalt = salt,
                FechaRegistro = DateTime.UtcNow,
                EstadoUsuarioId = 1, 
                TipoDocumentoId = 1
                // Agrega más campos si es necesario
            };

            // Insertar en base de datos
            bool resultSave = _usuarioRepository.Save(usuario);
            

            return (true, null);
        }

        private void CrearHashConSalt(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion

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
