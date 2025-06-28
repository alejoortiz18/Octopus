using Business.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using Models.Model.Usuario;
using Newtonsoft.Json;
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
            isAuthController = true;
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
            UsuarioReferidoViewModel nodo = BuscarNodo(raiz, usuarioId);
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

        [HttpPost]
        public IActionResult BuscarNodo(string rootJson, int usuarioId)
        {
            var root = JsonConvert.DeserializeObject<UsuarioReferidoViewModel>(rootJson);
            var nodo = BuscarNodo(root, usuarioId);
            return PartialView("_ReferidosList", nodo.Referidos);

        }

        // Método privado que recorre el árbol
        private UsuarioReferidoViewModel BuscarNodo(UsuarioReferidoViewModel root, int id)
        {
            if (root.UsuarioId == id)
                return root;

            foreach (var hijo in root.Referidos)
            {
                var encontrado = BuscarNodo(hijo, id);
                if (encontrado != null)
                    return encontrado;
            }

            return null;
        }

      

        private UsuarioReferidoViewModel GenerarRed(Usuario usuarioRoot)
        {
            // 1) Trae todos los DTOs de referidos
            List<RedPorReferidosByIdUsuarioDto> listRed =
                _redBusiness.GetTodaLaRedPorUsuarioIdAsync(usuarioRoot.UsuarioId);

            // 2) Crea el nodo raíz (el usuario que inició la consulta)
            var root = new UsuarioReferidoViewModel
            {
                UsuarioId = usuarioRoot.UsuarioId,
                Nombre = usuarioRoot.NombreCompleto,
                Nivel = 0,
                CodigoReferencia = usuarioRoot.CodigoReferencia,
                Referidos = new List<UsuarioReferidoViewModel>()
            };

            // 3) Crea diccionario de “Id de usuario” → “VM del referido”
            var diccionario = listRed
                .ToDictionary(
                   x => x.UsuarioId,
                   x => new UsuarioReferidoViewModel
                   {
                       UsuarioId = x.UsuarioId,
                       Nombre = x.NombreCompleto,
                       Nivel = x.Nivel,
                       CodigoReferencia = x.CodigoReferencia,
                       Referidos = new List<UsuarioReferidoViewModel>()
                   });

            // 4) Agrega **directos** al root
            foreach (var dto in listRed.Where(x => x.ReferenteID == usuarioRoot.UsuarioId))
            {
                root.Referidos.Add(diccionario[dto.UsuarioId]);
            }

            // 5) Construye el resto del árbol (nietos, biznietos...)
            foreach (var dto in listRed.Where(x => x.ReferenteID != usuarioRoot.UsuarioId))
            {
                if (diccionario.ContainsKey(dto.ReferenteID))
                {
                    diccionario[dto.ReferenteID]
                        .Referidos
                        .Add(diccionario[dto.UsuarioId]);
                }
            }

            return root;
        }

        private Usuario ValidarEstadoPerfil()
        {
            string? userEmail = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
            return _usuarioBusiness.ObtenerPorEmail(userEmail);

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
