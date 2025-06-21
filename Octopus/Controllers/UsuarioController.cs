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

            if (primerInicio.Equals(true))
            {
                ViewData["MensajeReferente"] = UsuarioMsnConstant.CodigoReferenteIsNull;
            }

            if (TempData["NoExisteCodigo"] != null && Convert.ToInt32(TempData["NoExisteCodigo"]) == 1)
            {
                ViewData["NoExisteCodigo"] = UsuarioMsnConstant.CodigoReferenteNoExiste;               
            }

            Usuario resultUser = _usuarioBusiness.ObtenerPorEmail(userEmail);

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

            var listBancosResult = _bancosBusiness.ObtenerBancos();
            var listBancos = _mapper.Map<List<BancoDto>>(listBancosResult);
            var listTipoCuenta = _bancosBusiness.GetListTipoCuenta();
            List<DetalleTipoCuenta> tipoCuenta = _mapper.Map<List<DetalleTipoCuenta>>(listTipoCuenta);

            var model2 = new PerfilUsuarioViewModel
            {
                DatosPersonales = model

            };

            model2.DatosBancarios.Bancos.AddRange(listBancos);
            model2.TipoBancarios.Tipocuenta.AddRange(tipoCuenta);
            model2.UsuarioId = 12345;

            return View(model2);
        }

        [HttpPost]
        public IActionResult ActualizarPerfil(PerfilUsuarioViewModel model)
        {
            Usuario responseUser = _usuarioBusiness.ObtenerPorCodigoReferencia(model.DatosPersonales.CodigoReferencia);
            if (responseUser == null) { 

                TempData["NoExisteCodigo"] =1;
                return RedirectToAction("Profile");

            }

            if (model.ActualizarDatosPersonales)
            {
                string codigoReferencia = _codigoHelper.GenerarCodigoUnico();

                model.DatosPersonales.CodigoReferencia = string.IsNullOrEmpty(model.DatosPersonales.CodigoReferencia)?
                                                         _codigoHelper.GenerarCodigoUnico() : model.DatosPersonales.CodigoReferencia;
                var resultSave = _usuarioBusiness.ActualizarUsuarioAsync(model.DatosPersonales);
            }


            return RedirectToAction("Profile", new { primerInicio = true });
        }
    }
}
