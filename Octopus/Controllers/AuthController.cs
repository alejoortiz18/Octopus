using Business.Interfaces;
using Constant;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Auth;
using System.Security.Claims;

namespace Octopus.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthBusiness _authBusiness;

        public AuthController(IAuthBusiness authService)
        {
            _authBusiness = authService;
        }

        private IActionResult RedirectIfAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult InicioSesion()
        {
            var redirectResult = RedirectIfAuthenticated();
            if (redirectResult != null)
            {
                return redirectResult;
            }
            ViewBag.Mensaje = null;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> InicioSesion(string username, string password)
        {
            try
            {
                var redirectResult = RedirectIfAuthenticated();
                if (redirectResult != null)
                {
                    return redirectResult;
                }

                LoginDto loginDto = new LoginDto
                {
                    Email = username,
                    Password = password
                };

                var loginExitoso = await _authBusiness.InicioSesionAsync(loginDto);
                var estado = loginExitoso.estado;
                var usuario = loginExitoso.usuario;
                if (estado==0)
                {
                    ViewData["InicioSesion"] = InicioSesionConstant.UsuarioEstadoCero;
                    return View();
                }
                if (estado == 1)
                {
                    ViewData["InicioSesion"] = (InicioSesionConstant.UsuarioEstadoUno);
                    return View();
                    //retornar un mensaje diciendo que el usuario esta desactivado
                }
                else if (estado == 2)
                {
                    ViewData["InicioSesion"] = (InicioSesionConstant.UsuarioEstadoDos);
                    return View();
                }
                else if (estado == 3)
                {
                    List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, usuario.NombreCompleto), // Fix for CS1061: Use the correct property from Usuario
                            new Claim(ClaimTypes.Email, username),
                            new Claim(ClaimTypes.Role, usuario.Rol.Nombre) // Assuming Rol.Nombre exists
                        };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true
                    };
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties: authProperties
                    );

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Mensaje = "Credenciales incorrectas";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "No pudo iniciar sesión";
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp(SignUpDto signUp)
        {
            if (!ModelState.IsValid)
            {
                // Devuelve la vista con el modelo para que se muestren los errores de validación
                return View(signUp);
            }

            var result = _authBusiness.SignUpAsync(signUp).Result;
            var success = result.Success;
            var errorMessage = result.ErrorMessage;

            // Puedes manejar el error aquí si lo necesitas, por ejemplo:
            if (!success)
            {
                ModelState.AddModelError("", errorMessage);
                return View(signUp);
            }
            else
            {
                ViewData["EnableUser"] = "Se ha creado el usuario, verifica el correo para continuar con el registo.";
                return View(signUp);
            }


        }

    }
}
