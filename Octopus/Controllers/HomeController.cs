using Business.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Octopus.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Octopus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioBusiness _usuarioBusiness;

        public HomeController(ILogger<HomeController> logger, IUsuarioBusiness usuario)
        {
            _logger = logger;
            _usuarioBusiness = usuario;
        }

        [HttpGet]
        public IActionResult Index(Guid? idSesion)
        {
            if (idSesion.HasValue)
            {

                string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
               
                ViewData["sesion"] = "";
            }
            ValidarEstadoPerfil();
            return View();
        }

     
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Eliminar la cookie de autenticación
            Response.Cookies.Delete(".AspNetCore.Cookies");

            // Evitar que la caché almacene la página anterior
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return RedirectToAction("InicioSesion", "Auth");
        }

        private void ValidarEstadoPerfil()
        {
            string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
            var resultUser =_usuarioBusiness.ObtenerPorEmail(userEmail);
        }
    }
}
