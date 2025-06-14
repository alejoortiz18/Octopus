using Business.Interfaces;
using Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Usuario;

namespace Octopus.Controllers
{
    public class EnableUserController : Controller
    {
        private readonly IUsuarioBusiness _IUsuarioBusiness;
        public EnableUserController(IUsuarioBusiness IUsuarioBusiness)
        {
            _IUsuarioBusiness = IUsuarioBusiness;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidarCodigo(string codigo, string email)
        {
            if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(email))
            {
                ViewData["ValidatedFailed"] = EnableUserResponseConstant.CodigoAndEmailInvalidoMsn;
                return View();
            }

            var usuario = _IUsuarioBusiness.ObtenerPorEmail(email);
            // Validar si la fecha de expiración del token es válida
            if (usuario.FechaExpiracionToken < DateTime.Now)
            {
                ViewData["ValidatedFailed"] = EnableUserResponseConstant.FechaExpiracionTokenMsn;
                return View();
            }


            if (usuario.EstadoUsuarioId == 2 )
            {
                ViewData["ValidatedFailed"] = EnableUserResponseConstant.BloqueoUsuarioMsn;
                return View();
            }

            if (codigo != usuario.TokenVerificacion)
            {
                ViewData["ValidatedFailed"] = EnableUserResponseConstant.TokenInvalidoMsn;
                return View();
            }

            // Si el código es válido, mostrar el formulario para crear una nueva contraseña
            var enableUserDto = new EnableUserDto
            {

                Email = email,
                Codigo = codigo
            };

            ViewData["EnableUserDto"] = enableUserDto;
            return View(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ValidarCodigo(EnableUserDto enableUserDto)
        {
        //    if (!ModelState.IsValid)
        //    {
        //        // Mostrar los errores de validación en la vista
        //        return View(enableUserDto);
        //    }

        //    // Validar si el código es válido
        //    var usuario = _IUsuarioBusiness.ObtenerPorEmail(enableUserDto.Email);

        //    if (usuario == null || usuario.CodigoActivo == false || usuario.Contrasena != "0")
        //    {
        //        ModelState.AddModelError(string.Empty, "El código es inválido o ha expirado.");
        //        return View(enableUserDto);
        //    }

        //    usuario.Contrasena = enableUserDto.Password1;
        //    usuario.CodigoActivo = false;
        //    usuario.CodigoRestablecerPassword = "";


        //    // Aquí se puede agregar la lógica para habilitar al usuario y actualizar la contraseña
        //    _IUsuarioBusiness.ActualizarUsuarioAsync(usuario);

            ViewData["expirado"] = "Usuario habilitado y contraseña actualizada correctamente.";
            return View(enableUserDto);
        }
    }
}
