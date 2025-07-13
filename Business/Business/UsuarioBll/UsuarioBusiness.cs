using AutoMapper;
using Business.Interfaces;
using Constant;
using Data.Interfaces;
using Helpers.Interfaces;
using Models.Dto.Comision;
using Models.Dto.Usuario;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Business.UsuarioBll
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IMapper _mapper;
        private ICodigoHelper _codigoHelper;
        private IEmailHelper _email;
        private IComisionRepository _comisionRepository;
        private IConfiguracionSistemaRepositorio _configuracionSistemaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository
            ,IMapper maper
            ,ICodigoHelper codigo
            ,IEmailHelper email
            , IComisionRepository comision
            , IConfiguracionSistemaRepositorio confgSistema)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = maper;
            _codigoHelper = codigo;
            _email = email;
            _comisionRepository = comision;
            _configuracionSistemaRepository = confgSistema;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return _usuarioRepository.ObtenerPorEmail(email);
        }

        public Task<bool> ActualizarUsuarioAsync(Usuario usuario)
        {
            return _usuarioRepository.ActualizarUsuarioAsync(usuario);
        }

        public Task<bool> ActualizarUsuarioAsync(DatosPersonalesUsuarioDto usuario, int? usuairoID =0)
        {
            var resultUpdate = _usuarioRepository.ActualizarUsuarioAsync(usuario);

            if (resultUpdate.Result)
            {
                List<ComisionDto> resultComision = _comisionRepository.BuscarNivelesSuperioesComisiones((int)usuairoID);
                var configuracionSistema = _configuracionSistemaRepository.ObtenerConfiguracionSistema();
                if (resultComision.Count > 0)
                {
                    List<Comision> listComision = new List<Comision>();
                    foreach (var item in resultComision)
                    {
                        Comision comision = new Comision();
                        comision.UsuarioId = item.UsuarioID;
                        comision.ReferidoId = (int)usuairoID;
                        comision.Monto = (Convert.ToInt16(configuracionSistema.Valor) * item.Porcentaje) / 100;
                        comision.FechaGeneracion = DateTime.Now;
                        comision.Nivel = item.Nivel;
                        comision.EstadoPagoId = 2; // Pendiente
                        comision.FechaPago = ObtenerProximoViernes();
                        listComision.Add(comision);
                    }
                    _comisionRepository.IngresarComisiones(listComision);
                }
                else
                {
                }
            }

            return resultUpdate;
        }

        public Usuario ObtenerPorId(int Id)
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

        public Usuario ObtenerPorCodigoReferencia(string codigoReferencia)
        {
            return _usuarioRepository.ObtenerPorCodigoReferencia(codigoReferencia);
        }

        public bool Save(Usuario usuario)
        {
           return _usuarioRepository.Save(usuario);

        }

        public Usuario ObtenerCuentaApoyo() { 
            return _usuarioRepository.ObtenerCuentaApoyo();
        }

        #region METODOS PRIVADOS
        public static DateTime ObtenerProximoViernes()
        {
            DateTime hoy = DateTime.Today;
            // DayOfWeek.Friday == 5, Sunday == 0, Monday == 1, ..., Saturday == 6
            int diferencia = ((int)DayOfWeek.Friday - (int)hoy.DayOfWeek + 7) % 7;

            // Si hoy es viernes, %7 da 0 → queremos avanzar 7 días
            if (diferencia == 0)
                diferencia = 7;

            return hoy.AddDays(diferencia);
        }
        #endregion
    }
}
