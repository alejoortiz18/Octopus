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

    }
}
