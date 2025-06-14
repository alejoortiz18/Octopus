using Business.Interfaces;
using Constant;
using Helpers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Models.Dto.Usuario;

namespace Octopus.Controllers
{
    public class EnableUserController : Controller
    {
        private readonly IUsuarioBusiness _IUsuarioBusiness;
        private bool isMsn;

        public EnableUserController(IUsuarioBusiness IUsuarioBusiness )
        {
            _IUsuarioBusiness = IUsuarioBusiness;
            

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidarCodigo(string codigo, string email)
        {
            var model = new EnabledUserDto
            {
                Email = email,
                Codigo = codigo
            };

            if (TempData["reenviarCodigo"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["reenviarCodigo"].ToString());
                return View(model);

            }

            string mensaje = ValidarCodigoInterno(codigo,  email);
            if (!mensaje.IsNullOrEmpty())
            {
                ViewData["EnableUserDto"] = null;
                ModelState.AddModelError(string.Empty, mensaje);
                return View(model);
            }

            ViewData["EnableUserDto"] = null;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ValidarCodigo(EnabledUserDto modelUser)
        {
            string mensaje = ValidarCodigoInterno(modelUser.Codigo, modelUser.Email);
            if (!mensaje.IsNullOrEmpty())
            {
                ViewData["EnableUserDto"] = null;
                ModelState.AddModelError(string.Empty, mensaje);
                return View(modelUser);
            }
           

            ViewData["expirado"] = "Usuario habilitado y contraseña actualizada correctamente.";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ReenviarCodigo(EnabledUserDto modelUser)
        {
            bool response = _IUsuarioBusiness.ReenviarCodigo(modelUser);
            if (!response)
            {
                ModelState.AddModelError(string.Empty, CodigoResponseConstant.RestablecerCodigoFallido);
                return View("ValidarCodigo", modelUser);
            }
            TempData["reenviarCodigo"] = CodigoResponseConstant.RestablecerCodigoOK;
            return RedirectToAction("ValidarCodigo", new { codigo = modelUser.Codigo, email = modelUser.Email });

        }



        private string ValidarCodigoInterno(string codigo, string email)
        {
            if (string.IsNullOrEmpty(codigo) || string.IsNullOrEmpty(email))
            {
                return EnableUserResponseConstant.CodigoAndEmailInvalidoMsn;
            }

            var usuario = _IUsuarioBusiness.ObtenerPorEmail(email);
            if (usuario == null)
            {
                return EnableUserResponseConstant.CodigoAndEmailInvalidoMsn;
            }

            if (usuario.FechaExpiracionToken < DateTime.Now)
            {
                return EnableUserResponseConstant.FechaExpiracionTokenMsn;
            }

            if (usuario.EstadoUsuarioId == 2)
            {
                return EnableUserResponseConstant.BloqueoUsuarioMsn;
            }

            if (codigo != usuario.TokenVerificacion)
            {
                return EnableUserResponseConstant.TokenInvalidoMsn;
            }

            // Si llega aquí, no hay mensaje de error
            return string.Empty;
        }


    }
}
