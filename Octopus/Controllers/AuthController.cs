using AutoMapper;
using Business.Interfaces;
using Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dto.Auth;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System.Security.Claims;

namespace Octopus.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthBusiness _authService;
       // private readonly IMapper _mapper;

        public AuthController(IAuthBusiness authService/*, IMapper mapper*/)
        {
            _authService = authService;
            //_mapper = mapper;
        }

        private IActionResult RedirectIfAuthenticated()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Entrenamiento");
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

                //Task<Usuario> loginExitosoTask = new Task<Usuario>();

                //// var loginExitoso = _authService.InicioSesionAsync(loginDto).Result;

                //// if (loginExitoso != null)
                //if (true )
                //{
                //    List<Claim> claims = new List<Claim>
                //    {
                //        new Claim(ClaimTypes.Name, loginExitoso.Nombre),
                //        new Claim(ClaimTypes.Email, username),
                //        new Claim(ClaimTypes.Role, loginExitoso.IdPerfilNavigation.Nombre)
                //    };

                //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //    AuthenticationProperties authProperties = new AuthenticationProperties
                //    {
                //        AllowRefresh = true
                //    };
                //    await HttpContext.SignInAsync(
                //     CookieAuthenticationDefaults.AuthenticationScheme,
                //     new ClaimsPrincipal(claimsIdentity),
                //     properties: authProperties
                //    );

                //   return RedirectToAction("Index", "Entrenamiento");
                //}

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

            var (hash, salt) = PasswordHelper.CrearHash(signUp.Password);

            var usuario = new Usuario
            {
                NombreCompleto = signUp.Username,
                Email = signUp.Email,
                ContrasenaHash = hash,
                ContrasenaSalt = salt,
                FechaRegistro = DateTime.Now,
                EstadoUsuarioId = 1, 
                CambioContrasena = false,
                Bloqueado = false
                // Otros campos requeridos por la tabla
            };
            return RedirectToAction("InicioSesion");
        }


        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult ResetPasswordValid(ResetPasswordResponseDto model)
        //{
        //    ResetPasswordDto datoDto = new ResetPasswordDto();

        //    if (!ModelState.IsValid)
        //    {
        //        datoDto.Email = model.Email;
        //        datoDto.IsGenerated = true;
        //        datoDto.Message = "";
        //        ViewData["DatoDto"] = datoDto;
        //        return View("ResetPassword", model);
        //    }

        //    UserCod userCod = new UserCod
        //    {
        //        Email = model.Email,
        //        Contrasena = model.NewPassword,
        //        CodigoRestablecerPassword = model.Codigo,
        //        CodigoActivo = false
        //    };

        //    bool result = _authService.ChangePassword(userCod);

        //    if (!result)
        //    {

        //        datoDto.Email = model.Email;
        //        datoDto.IsGenerated = true;
        //        datoDto.Message = "";
        //        ViewData["DatoDto"] = datoDto;
        //        ModelState.AddModelError(string.Empty, "Código inválido.");
        //        return View("ResetPassword", model);


        //    }

        //    return RedirectToAction("InicioSesion");
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(string username)
        //{

        //    var resultado = await _authService.GenerarCodigoRestablecimientoAsync(username);
        //    ResetPasswordDto datoDto = new ResetPasswordDto();

        //    datoDto.Email = resultado ? username : null;
        //    datoDto.IsGenerated = resultado;
        //    datoDto.Message = resultado ? "Se ha enviado un correo con el código de restablecimiento"
        //                                : "Error al enviar código";

        //    ViewData["DatoDto"] = datoDto;
            //return View();
        //}

    }
}
