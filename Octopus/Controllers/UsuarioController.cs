using AutoMapper;
using Business.Interfaces;
using Constant;
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
        private readonly IUsuarioBusiness _usuarioMsnConstant;
        private readonly IBancosBusiness _bancosBusiness;
        private readonly IMapper _mapper;


        public UsuarioController(IUsuarioBusiness usuarioBusiness, IBancosBusiness bancosBusiness, IMapper mapper)
        {
            _usuarioMsnConstant = usuarioBusiness;
            _bancosBusiness = bancosBusiness;
            _mapper = mapper;
        }

        public IActionResult Profile(bool? primerInicio =false)
        {
            string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();

            if (primerInicio.Equals(true))
            {
                ViewData["MensajeReferente"] = UsuarioMsnConstant.CodigoReferenteIsNull;
            }

            Usuario resultUser = _usuarioMsnConstant.ObtenerPorEmail(userEmail);

            var model = new DatosPersonalesUsuarioDto
            {
                CodigoReferencia = resultUser.CodigoReferencia ==null?"": resultUser.CodigoReferencia,
                EstadoUsuario = resultUser.EstadoUsuario==null?0: resultUser.EstadoUsuario.EstadoUsuarioId,
                TipoDocumento = resultUser.EstadoUsuarioId,
                NumeroDocumento = resultUser.NumeroDocumento==null?"": resultUser.NumeroDocumento,
                NombreCompleto = resultUser.NombreCompleto == null || resultUser.NombreCompleto==""? resultUser.Email: resultUser.NombreCompleto,
                Email = resultUser.Email,
                FechaRegistro = resultUser.FechaRegistro,
                FechaHabilitacion = (DateTime)resultUser.FechaHabilitacion
            };

            var listBancosResult = _bancosBusiness.ObtenerBancos();
            var listBancos = _mapper.Map<List<BancoDto>>(listBancosResult);

        

            List<DetalleTipoCuenta> tipoCuenta = new List<DetalleTipoCuenta>
            {
                new DetalleTipoCuenta
                {
                    Nombre = "Corriente",
                 TipoCuentaBancariaId = 1   
                },
                new DetalleTipoCuenta
                {
                 TipoCuentaBancariaId = 2,
                    Nombre = "Ahorros"

                }
                // Puede agregar más bancos aquí según sea necesario
            };

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
            // Accede a model.DatosPersonales y model.DatosBancarios
            // Procesar y guardar los datos

            return RedirectToAction("Profile");
        }
    }
}
