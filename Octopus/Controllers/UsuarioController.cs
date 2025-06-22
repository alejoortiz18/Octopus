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


        public UsuarioController(IUsuarioBusiness usuarioBusiness, IBancosBusiness bancosBusiness, IMapper mapper,ICodigoHelper codHelper)
        {
            _usuarioBusiness = usuarioBusiness;
            _bancosBusiness = bancosBusiness;
            _mapper = mapper;
            _codigoHelper = codHelper;
        }

        public IActionResult Profile(bool? primerInicio =false)
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


            var model = new DatosPersonalesUsuarioDto
            {
                CodigoReferencia = resultUser.CodigoReferencia ==null?"": resultUser.CodigoReferencia,
                EstadoUsuario = resultUser.EstadoUsuario==null?0: resultUser.EstadoUsuario.EstadoUsuarioId,
                TipoDocumento = resultUser.EstadoUsuarioId,
                NumeroDocumento = resultUser.NumeroDocumento==null?"": resultUser.NumeroDocumento,
                NombreCompleto = resultUser.NombreCompleto == null || resultUser.NombreCompleto==""? resultUser.Email: resultUser.NombreCompleto,
                Email = resultUser.Email,
                NumeroCelular = resultUser.NumeroCelular,
                FechaRegistro = resultUser.FechaRegistro,
                FechaHabilitacion = (DateTime)resultUser.FechaHabilitacion
            };

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
            model2.TipoBancarios.Tipocuenta.AddRange(tipoCuenta);
            model2.TipoBancarios.Nombre = resultUser.TipoCuentaBancaria?.Nombre != null ? resultUser.TipoCuentaBancaria.Nombre : null;
            model2.UsuarioId = resultUser.UsuarioId;

            return View(model2);
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(PerfilUsuarioViewModel model)
        {
            Usuario resultUsuario = _usuarioBusiness.ObtenerPorId(model.UsuarioId); 
            if (model.ActualizarDatosPersonales)
            {
                Usuario responseUser = _usuarioBusiness.ObtenerPorCodigoReferencia(model.DatosPersonales.CodigoReferente);
                if (responseUser == null) { 

                    TempData["NoExisteCodigo"] =1;
                    return RedirectToAction("Profile");

                }
                if (model.DatosPersonales.CodigoReferente ==null)
                {
                    model.DatosPersonales.ReferenteId = _usuarioBusiness.ObtenerCuentaApoyo().UsuarioId;
                }
                if (resultUsuario.ReferenteId !=null)
                {
                    model.DatosPersonales.ReferenteId = (int)resultUsuario.ReferenteId;
                }
                 _usuarioBusiness.ActualizarUsuarioAsync(model.DatosPersonales);
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


           resultUsuario.BancoId = model.DatosBancarios.Banco == null ? null: model.DatosBancarios.Banco;
           resultUsuario.NumeroCuentaBancaria = model.DatosBancarios.NumeroCuenta ==null ? null: model.DatosBancarios.NumeroCuenta;
           resultUsuario.TipoCuentaBancariaId = model.TipoBancarios.Nombre==""||
                                               model.TipoBancarios.Nombre == null? null:
                                                                                   Convert.ToInt16( model.TipoBancarios.Nombre);
            _usuarioBusiness.ActualizarUsuarioAsync(resultUsuario);

            return RedirectToAction("Profile", new { primerInicio = true });

        }

       
    }
}
