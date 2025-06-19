using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Usuario;

namespace Octopus.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        public IActionResult Profile()
        {
            var model = new DatosPersonalesUsuarioDto
            {
                
                EstadoUsuario = 1,
                TipoDocumento = 1,
                NumeroDocumento = "123456789",
                NombreCompleto = "Alexa Liras",
                Email = "alexa@creative-tim.com",
                FechaRegistro = DateTime.Now.AddMonths(-2),
                FechaHabilitacion = DateTime.Now.AddDays(-10)
            };

            return View(model);
        }
    }
}
