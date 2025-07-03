using AutoMapper;
using Business.Interfaces;
using Constant;
using Helpers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Banco;
using Models.Dto.Usuario;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Models.Model.Usuario;
using System.Security.Claims;

namespace Octopus.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioBusiness _usuarioBusiness;
        private readonly IBancosBusiness _bancosBusiness;
        private readonly IMapper _mapper;
        private readonly ICodigoHelper _codigoHelper;
        private readonly IRedBusiness _redReferidoBusiness;

        public UsuarioController(IUsuarioBusiness usuarioBusiness
            ,IBancosBusiness bancosBusiness
            ,IMapper mapper
            ,ICodigoHelper codHelper
            ,IRedBusiness red)
        {
            _usuarioBusiness = usuarioBusiness;
            _bancosBusiness = bancosBusiness;
            _mapper = mapper;
            _codigoHelper = codHelper;
            _redReferidoBusiness = red;
        }

        [Route("mi-perfil")]
        public IActionResult Profile(bool? primerInicio =false, string? mensajeNoReferente = null)
        {
            
            
            string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();

            Usuario resultUser = _usuarioBusiness.ObtenerPorEmail(userEmail);



            if (primerInicio.Equals(true) && resultUser.ReferenteId == null)
            {
                ViewData["MensajeReferente"] = UsuarioMsnConstant.CodigoReferenteIsNull;
            }

            if (TempData["NoExisteCodigo"] != null && Convert.ToInt32(TempData["NoExisteCodigo"]) == 1)
            {
                ViewData["NoExisteCodigo"] = UsuarioMsnConstant.CodigoReferenteNoExiste;               
            }

            if (mensajeNoReferente != null)
            {
                string mensaje = mensajeNoReferente;

                if (mensaje == UsuarioMsnConstant.CodigoReferenteNoExiste)
                {
                    ViewData["mensajeNoReferente"] = UsuarioMsnConstant.CodigoReferenteNoExiste;
                }
                else if (mensaje == UsuarioMsnConstant.CodigoReferenteLleno)
                {
                    ViewData["mensajeNoReferente"] = UsuarioMsnConstant.CodigoReferenteLleno;
                }
                TempData["mensajeNoReferente"] = "";
            }


            var model = new DatosPersonalesUsuarioDto();

            
            model.CodigoReferencia = resultUser.CodigoReferencia == null ? "" : resultUser.CodigoReferencia;
            model.EstadoUsuario = resultUser.EstadoUsuario == null ? 0 : resultUser.EstadoUsuarioId;
            model.TipoDocumento = resultUser.EstadoUsuarioId;
            model.NumeroDocumento = resultUser.NumeroDocumento == null ? "" : resultUser.NumeroDocumento;
            model.NombreCompleto = resultUser.NombreCompleto == null || resultUser.NombreCompleto == "" ? resultUser.Email : resultUser.NombreCompleto;
            model.Email = resultUser.Email;
            model.NumeroCelular = resultUser.NumeroCelular;
            model.FechaRegistro = resultUser.FechaRegistro;
            if (resultUser.FechaHabilitacion != null)
            {
                model.FechaHabilitacion = (DateTime)resultUser.FechaHabilitacion;

            }
            

            if (resultUser.ReferenteId !=null)
            {
                var cod = _usuarioBusiness.ObtenerPorId((int)resultUser.ReferenteId);
                model.CodigoReferente = cod.CodigoReferencia;

            }

            var listBancosResult = _bancosBusiness.ObtenerBancos();
            var listBancos = _mapper.Map<List<BancoDto>>(listBancosResult);
            var listTipoCuenta = _bancosBusiness.GetListTipoCuenta();
            List<DetalleTipoCuenta> tipoCuenta = _mapper.Map<List<DetalleTipoCuenta>>(listTipoCuenta);

            var model2 = new PerfilUsuarioViewModel
            {
                DatosPersonales = model

            };

            model2.DatosBancarios.Bancos.AddRange(listBancos);

            model2.DatosBancarios.Nombre = resultUser.Banco?.Nombre != null ? resultUser.Banco.Nombre : null;
            model2.DatosBancarios.NumeroCuenta = resultUser.NumeroCuentaBancaria != null ? resultUser.NumeroCuentaBancaria : null;
            model2.TipoBancarios.Tipocuenta.AddRange(tipoCuenta);
            model2.TipoBancarios.Nombre = resultUser.TipoCuentaBancaria?.Nombre != null ? resultUser.TipoCuentaBancaria.Nombre : null;
            model2.UsuarioId = resultUser.UsuarioId;

            return View(model2);
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(PerfilUsuarioViewModel model)
        {
            Usuario resultUsuario = _usuarioBusiness.ObtenerPorId(model.UsuarioId);
            string mensajeNoReferente = null;
            bool registrado = false;
            if (model.ActualizarDatosPersonales)
            {
                string resultValidar = ValidarRedReferidos(model.DatosPersonales.CodigoReferente);
                if (resultValidar != "")
                {
                    if (resultValidar == UsuarioMsnConstant.CodigoReferenteNoExiste)
                    {
                        mensajeNoReferente = UsuarioMsnConstant.CodigoReferenteNoExiste;
                        return RedirectToAction(nameof(Profile), new
                        {
                            primerInicio = (bool?)false,   // cast opcional si prefieres que sea explícito
                            mensajeNoReferente             // C# 7+: la propiedad toma este mismo nombre
                        });
                    }
                    else if (resultValidar == UsuarioMsnConstant.CodigoReferenteLleno)
                    {
                        mensajeNoReferente = UsuarioMsnConstant.CodigoReferenteLleno;
                        return RedirectToAction(nameof(Profile), new
                        {
                            primerInicio = (bool?)false,   // cast opcional si prefieres que sea explícito
                            mensajeNoReferente             // C# 7+: la propiedad toma este mismo nombre
                        });
                    }
                }

                bool ingresoCodigo = true;
                if (model.DatosPersonales.CodigoReferente ==null)
                {
                    ingresoCodigo = false;
                }

                Usuario responseUser = new Usuario();
                if (ingresoCodigo == false)
                {
                    model.DatosPersonales.ReferenteId = _usuarioBusiness.ObtenerCuentaApoyo().UsuarioId;
                    registrado = _usuarioBusiness.ActualizarUsuarioAsync(model.DatosPersonales).Result; 
                }
                else
                {
                    responseUser = _usuarioBusiness.ObtenerPorCodigoReferencia(model.DatosPersonales.CodigoReferente);

                    if (responseUser == null) { 

                        TempData["NoExisteCodigo"] =1;
                        return RedirectToAction("Profile");
                    }
                    model.DatosPersonales.ReferenteId = responseUser.UsuarioId;
                    registrado = _usuarioBusiness.ActualizarUsuarioAsync(model.DatosPersonales).Result; 
                }
                if (registrado)
                {
                    RedReferido red = new RedReferido
                    {
                        UsuarioId = model.UsuarioId,
                        ReferenteId = model.DatosPersonales.ReferenteId,
                        Nivel = 0,
                        FechaVinculacion = DateTime.Now
                    };
                    _redReferidoBusiness.Insert(red);
                    TempData["ActualizarUsuario"] = "El usuario se ha actualizado Correctamente";
                }
                else
                {
                    TempData["ActualizarUsuario"] = "No se pudo actualizar el usuario";
                    return RedirectToAction("Profile");
                }
                return RedirectToAction("Profile", new { primerInicio = true });
            }

            if(model.DatosBancarios.NumeroCuenta == null 
                && model.DatosBancarios.Banco == null
                && (model.TipoBancarios.Nombre == null || model.TipoBancarios.Nombre == ""))
            {
                _usuarioBusiness.ActualizarUsuarioAsync(model.DatosPersonales);
                TempData["NoSeleccionoDatosBanco"] = 0;
                return RedirectToAction("Profile");
            }

            if (resultUsuario.BancoId == null )
            {
               resultUsuario.BancoId = model.DatosBancarios.Banco == null ? null: Convert.ToInt16( model.DatosBancarios.Nombre);
            }
            if (resultUsuario.TipoCuentaBancariaId == null )
            {
               resultUsuario.TipoCuentaBancariaId = model.TipoBancarios.Nombre==""||
                                               model.TipoBancarios.Nombre == null? null:
                                                                                   Convert.ToInt16( model.TipoBancarios.Nombre);
            }
            
            resultUsuario.NumeroCuentaBancaria = model.DatosBancarios.NumeroCuenta ==null ? null: model.DatosBancarios.NumeroCuenta;
            _usuarioBusiness.ActualizarUsuarioAsync(resultUsuario);

            return RedirectToAction("Profile", new { primerInicio = true });

        }

        private string ValidarRedReferidos(string codigo)
        {
            var usuario = _usuarioBusiness.ObtenerPorCodigoReferencia(codigo);
            if (usuario == null)
            {
                return UsuarioMsnConstant.CodigoReferenteNoExiste;
            }
            var red = _redReferidoBusiness.GetTodaLaRedPorUsuarioIdAsync(usuario.UsuarioId);
            if (red.Count >= 5)
            {
                return UsuarioMsnConstant.CodigoReferenteLleno;
            }
            else
            {
                return "";
            }
        }

    }
}
