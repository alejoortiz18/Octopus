using Business.Interfaces;
using Constant;
using Helpers;
using Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Dto.Auth;
using Models.Dto.Usuario;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System.Security.Claims;
using System.Security.Policy;

namespace Octopus.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IUsuarioBusiness _userBusiness;
        private readonly IEmailHelper _emailHelper;
        private readonly ICodigoHelper _codigoHelper;

        public AuthController(IAuthBusiness authService, IUsuarioBusiness userBusiness, IEmailHelper emailHelper , ICodigoHelper codigoHelper)
        {
            _authBusiness = authService;
            _userBusiness = userBusiness;
            _emailHelper = emailHelper;
            _codigoHelper = codigoHelper;
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
                            //new Claim(ClaimTypes.Role, usuario.Rol.Tipo) // Assuming Rol.Tipo exists
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

                    if (loginExitoso.usuario.ReferenteId == null)
                    {
                        return RedirectToAction("Profile", "Usuario", new { primerInicio = true });
                    }

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
            ResetPasswordDto dto = new ResetPasswordDto();
            
            ViewData["DatoDto"] = dto;
            ViewData["EnableUser"] ="";
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
                ViewData["EnableUser"] = RegistroConstant.RegistroOK;
                return View(signUp);
            }


        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            return View();
        }

      
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordResponseDto username)
        {
            

            if(username.Codigo == "-1" && username.NewPassword.IsNullOrEmpty())
            {
                username.Codigo = null;
                var result =  _userBusiness.ObtenerPorEmail(username.Email);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, ResetPasswordConstant.EmailNoExiste);
                    return View();
                }
                if (result.EstadoUsuarioId ==2)
                {
                    ModelState.AddModelError(string.Empty, ResetPasswordConstant.EmailSuspendido);
                    return View();
                }
                result.TokenVerificacion = _codigoHelper.GenerarCodigoUnico();
                result.FechaExpiracionToken = DateTime.Now.AddHours(2);

                 bool resultUpdate = _userBusiness.ActualizarUsuarioAsync(result).Result;

                if (resultUpdate)
                {
                    var rsult = _emailHelper.EnviarCorreoAsync(username.Email,
                                                             result.TokenVerificacion,
                                                             EmailConstant.AsuntoRestablecerContrasena,
                                                             EmailConstant.CuerpoRestablecerContrasena);

                    if (rsult.IsCompletedSuccessfully)
                    {
                        ResetPasswordDto datoDto = new ResetPasswordDto();

                        datoDto.Email = username.Email;
                        datoDto.IsGenerated = true;
                        datoDto.Message = "Se ha enviado un correo con el código de restablecimiento";
                        ViewData["DatoDto"] = datoDto;
                        ViewData["EnableUser"] = datoDto.Message;
                    }
                }
            }
            else
            {
                if (!username.NewPassword.Equals(username.RepeatNewPassword))
                {
                    ModelState.AddModelError(string.Empty, "Las contraseñas no son iguales");
                }else if ( username.Codigo == "" || username.Codigo == null)
                {
                    ModelState.AddModelError(string.Empty, "El codigo es obligatorio");
                }
                else
                {
                    var resultUser = _userBusiness.ObtenerPorEmail(username.Email);

                    resultUser.TokenVerificacion = null;
                    resultUser.FechaExpiracionToken = null;
                    resultUser.CambioContrasena = true;

                    var (hash, salt) = PasswordHelper.CrearHash(username.NewPassword);
                    resultUser.ContrasenaHash = hash;
                    resultUser.ContrasenaSalt = salt;
                    resultUser.RolId = 1;
                    resultUser.FechaUltimoAcceso = DateTime.Now;
                    var responseUpdate = _userBusiness.ActualizarUsuarioAsync(resultUser).Result;

                    ResetPasswordDto datoDto = new ResetPasswordDto();

                    datoDto.Email = username.Email;
                    datoDto.IsGenerated = true;
                    datoDto.Message = "Se ha establecido la cuenta correctamente";
                    ViewData["DatoDto"] = datoDto;
                    ViewData["EnableUser"] = datoDto.Message;
                }
            }


                return View();
        }


       

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

        //    //UserCod userCod = new UserCod
        //    //{
        //    //    Email = model.Email,
        //    //    Contrasena = model.NewPassword,
        //    //    CodigoRestablecerPassword = model.Codigo,
        //    //    CodigoActivo = false
        //    //};

        //    //bool result = _authService.ChangePassword(userCod);

        //    //if (!result)
        //    if (true)
        //    {

        //        datoDto.Email = model.Email;
        //        datoDto.IsGenerated = true;
        //        datoDto.Message = "";
        //        ViewData["DatoDto"] = datoDto;
        //        ModelState.AddModelError(string.Empty, "Código inválido.");
        //        return View("ResetPassword", model);


        //    }
        //}

    }
}
