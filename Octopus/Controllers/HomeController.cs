using Business.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Models.Model.Usuario;
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
        private readonly IRedBusiness _redBusiness;

        public HomeController(ILogger<HomeController> logger, IUsuarioBusiness usuario, IRedBusiness redBusiness)
        {
            _logger = logger;
            _usuarioBusiness = usuario;
            _redBusiness = redBusiness;
        }

        [HttpGet]
        public IActionResult Index(Guid? idSesion, bool? isAuthController = false)
        {
            if (idSesion.HasValue)
            {

                string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();

                ViewData["sesion"] = "";
            }
            var user = ValidarEstadoPerfil();
            GetReferidosPartial(user.UsuarioId, (bool)isAuthController);
            return View();
        }

        public IActionResult GetReferidosPartial(int usuarioId, bool isAuthController = false)
        {
            string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
            var usuarioActual = _usuarioBusiness.ObtenerPorEmail(userEmail);
            // Generas tu vista modelo jerárquico completo
            var raiz = GenerarRed(usuarioActual);
            // Encuentra el nodo clicado:
            var nodo = BuscarNodo(raiz, usuarioId);
            if (nodo == null)
            {
                UsuarioReferidoViewModel model = new UsuarioReferidoViewModel();
                model.Nombre = usuarioActual.NombreCompleto;
                model.UsuarioId = usuarioActual.UsuarioId;
                model.CodigoReferencia = usuarioActual.CodigoReferencia;
                model.Nivel = 0;
                model.Referidos = new List<UsuarioReferidoViewModel>();
                return View(model);
            }
            if (isAuthController)
            {
                return View(nodo);
            }
            return PartialView("_ReferidosList", nodo.Referidos);

            // En lugar de pasar nodo.Referidos a _TarjetaUsuario,
            // devolvemos la lista a _ReferidosList:
        }

        private UsuarioReferidoViewModel BuscarNodo(UsuarioReferidoViewModel root, int id)
        {
            if (root == null)
            {
                return null;
            }
            if (root.UsuarioId == id) return root;
            foreach (var hijo in root.Referidos)
            {
                var encontrado = BuscarNodo(hijo, id);
                if (encontrado != null) return encontrado;
            }
            return null;
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

        private Usuario ValidarEstadoPerfil()
        {
            string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
            return _usuarioBusiness.ObtenerPorEmail(userEmail);

        }

        private UsuarioReferidoViewModel GenerarRed(Usuario usuarioId)
        {

            List<RedPorReferidosByIdUsuarioDto> listRed = _redBusiness.GetTodaLaRedPorUsuarioIdAsync(usuarioId.UsuarioId);
          
         
            // Convertimos la lista a un diccionario de UsuarioReferidoViewModel
            var diccionarioUsuarios = listRed.ToDictionary(
                x => x.UsuarioId,
                x => new UsuarioReferidoViewModel
                {
                    UsuarioId = x.UsuarioId,
                    Nombre = x.NombreCompleto,
                    Nivel = x.Nivel,
                    CodigoReferencia = x.CodigoReferencia,
                    Referidos = new List<UsuarioReferidoViewModel>()
                });

            UsuarioReferidoViewModel? root = null;

            // Construimos la jerarquía
            foreach (var item in listRed)
            {
                if (item.UsuarioId == item.ReferenteID)
                {
                    // Es el nodo raíz (ej. Apoyo)
                    root = diccionarioUsuarios[item.UsuarioId];
                }
                else if (diccionarioUsuarios.ContainsKey(item.ReferenteID))
                {
                    // Se lo agregamos a su referente
                    diccionarioUsuarios[item.ReferenteID].Referidos.Add(diccionarioUsuarios[item.UsuarioId]);
                }
            }

            return root!;

        }




    }
}
