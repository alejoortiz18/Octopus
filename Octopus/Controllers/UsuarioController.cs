using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Banco;
using Models.Dto.Usuario;
using Models.Model.Usuario;

namespace Octopus.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public IActionResult Profile()
        {
            var model = new DatosPersonalesUsuarioDto
            {
                CodigoReferencia = "",
                EstadoUsuario = 1,
                TipoDocumento = 1,
                NumeroDocumento = "123456789",
                NombreCompleto = "Alexa Liras",
                Email = "alexa@creative-tim.com",
                FechaRegistro = DateTime.Now.AddMonths(-2),
                FechaHabilitacion = DateTime.Now.AddDays(-10)
            };

            List<BancoDto> bancos = new List<BancoDto>
            {
                new BancoDto
                {
                    BancoId = 1,
                    Nombre = "Banco de bogota",
                    Codigo = "BE001",
                    Activo = true
                },
                new BancoDto
                {
                    BancoId = 2,
                    Nombre = "Bancolombia",
                    Codigo = "BP002",
                    Activo = true
                }
                // Puede agregar más bancos aquí según sea necesario
            };
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

            model2.DatosBancarios.Bancos.AddRange(bancos);
            model2.TipoBancarios.Tipocuenta.AddRange(tipoCuenta);


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
