using Business.Interfaces;
using Constant;
using Data.Interfaces;
using Helpers;
using Helpers.Interfaces;
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
        private ICodigoHelper _codigoHelper;
        private readonly IAuthRepository _authRepository;
        private readonly IEmailHelper _emailHelper;


        public AuthBusiness(IUsuarioRepository usuarioRepository
                            ,IEmailHelper emailHelper
                            ,IAuthRepository authRepository
                            ,ICodigoHelper codigoHelper)
        {
            _usuarioRepository = usuarioRepository;
            _emailHelper = emailHelper;
            _authRepository = authRepository;
            _codigoHelper = codigoHelper;
        }

        #region METODOS PARA LOGIN 

        public async Task<(int estado, Usuario usuario)> InicioSesionAsync(LoginDto user)
        {

            Usuario usuario =  _authRepository.LoginAsync(user.Email);
            bool puedeIniciarSesion = false;
            if (usuario == null)
            {
                return (0,null);
            }

            var usuarioOK = PasswordHelper.VerificarPassword(user.Password,usuario.ContrasenaHash,usuario.ContrasenaSalt);
            if (usuarioOK )
            {
                switch (usuario.EstadoUsuarioId)
                {
                    case 1:
                        return (1, usuario);
                    case 2:
                        return (2, usuario); 
                    case 3:
                        puedeIniciarSesion = true;
                        return (3, usuario); 
                }

            }


            return (0,null);
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
                return (false, UsuarioMsnConstant.CorreoExiste);
            }

            // Generar Salt y Hash
            var (hash, salt) = PasswordHelper.CrearHash(signUpDto.Password);

            // Crear entidad de usuario
            Usuario usuario = new Usuario
            {
                NombreCompleto = signUpDto.Username,
                Email = signUpDto.Email,
                ContrasenaHash = hash,
                ContrasenaSalt = salt,
                FechaRegistro = DateTime.Now,
                EstadoUsuarioId = 1, 
                TipoDocumentoId = 1,
                RolId = 1,
                TokenVerificacion = _codigoHelper.GenerarCodigoUnico(),
                FechaExpiracionToken = DateTime.Now.AddHours(2), 
                // Generar un código de referencia único
                // Agrega más campos si es necesario
            };

            // Insertar en base de datos
            bool resultSave = _usuarioRepository.Save(usuario);
            if (resultSave)
            {
                _emailHelper.EnviarCorreoCrearUsuarioNuevoAsync(usuario, EmailConstant.AsuntoUsuarioNuevo,EmailConstant.CuerpoCreateUsuario);
            }

            return (true, null);
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
