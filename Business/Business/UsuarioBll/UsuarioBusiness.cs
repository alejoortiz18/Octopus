using AutoMapper;
using Business.Interfaces;
using Constant;
using Data.Interfaces;
using Helpers.Interfaces;
using Models.Dto.Usuario;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Business.UsuarioBll
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IMapper _mapper;
        private ICodigoHelper _codigoHelper;
        private IEmailHelper _email;

        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioBusiness(IUsuarioRepository usuarioRepository, IMapper maper, ICodigoHelper codigo, IEmailHelper email)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = maper;
            _codigoHelper = codigo;
            _email = email;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return _usuarioRepository.ObtenerPorEmail(email);
        }

        public Task<bool> ActualizarUsuarioAsync(Usuario usuario)
        {
            return _usuarioRepository.ActualizarUsuarioAsync(usuario);
        }

        public Usuario ObtenerPorId(Guid Id)
        {
            return _usuarioRepository.ObtenerPorId(Id);
        }

        public bool ReenviarCodigo(EnabledUserDto modelUser)
        {
            var (result, User) = _usuarioRepository.ReenviarCodigo(modelUser);
            if (result)
            {
                _email.EnviarCorreoCrearUsuarioNuevoAsync(User, EmailConstant.AsuntoRestablecerCodigo, EmailConstant.CuerpoRestablecerCodigo);
            }
            return result;

        }

        //public bool SaveProfile(CreateProfileUserDto user)
        //{


        //    var userResult = _usuarioRepository.ObtenerPorEmail(user.Correo);
        //    if (userResult == null)
        //    {
        //        var usuario = _mapper.Map<Usuario>(user);
        //        usuario.Tipo = $"{usuario.Tipo} {user.Apellido}";
        //        usuario.PerfilActivo = true;
        //        usuario.CodigoRestablecerPassword = _codigoHelper.GenerarCodigoUnico();
        //        usuario.CodigoActivo = true;
        //        usuario.IdPerfil = Guid.Parse("07744E40-B12B-4B84-99B0-154282ECE7BB");
        //        usuario.IdTipoDocumento = 1;
        //        usuario.Contrasena = "0";
        //        bool result = _usuarioRepository.SaveProfile(usuario);
        //        if (result)
        //        {
        //            _email.EnviarCorreoCrearUsuarioAsync(usuario, Email.AsuntoCreateUsuario, Email.CuerpoCreateUsuario);
        //        }
        //        return result;

        //    }
        //    return false;

        //}
    }
}
