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
            var listRed = _redBusiness.GetTodaLaRedPorUsuarioIdAsync(usuarioRoot.UsuarioId);

            // Agrupar por ReferenteID
            var hijosPorReferente = listRed
                .GroupBy(x => x.ReferenteID)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Diccionario para evitar duplicados
            var nodosCreados = new Dictionary<int, UsuarioReferidoViewModel>();

            UsuarioReferidoViewModel CrearNodo(int usuarioId, int nivel)
            {
                // Si ya se creó este usuario, lo retornamos directamente (evita duplicados)
                if (nodosCreados.TryGetValue(usuarioId, out var existente))
                    return existente;

                RedPorReferidosByIdUsuarioDto? datos = listRed.FirstOrDefault(x => x.UsuarioId == usuarioId);

                // Si no existe en la lista (caso especial: root), usamos el root
                string nombre = datos?.NombreCompleto ?? usuarioRoot.NombreCompleto;
                string codigo = datos?.CodigoReferencia ?? usuarioRoot.CodigoReferencia;

                var nodo = new UsuarioReferidoViewModel
                {
                    UsuarioId = usuarioId,
                    Nombre = nombre,
                    Nivel = nivel,
                    CodigoReferencia = codigo,
                    Referidos = new List<UsuarioReferidoViewModel>()
                };

                nodosCreados[usuarioId] = nodo;

                if (hijosPorReferente.TryGetValue(usuarioId, out var hijos))
                {
                    foreach (var hijo in hijos)
                    {
                        if (!nodosCreados.ContainsKey(hijo.UsuarioId))
                        {
                            var hijoNodo = CrearNodo(hijo.UsuarioId, nivel + 1);
                            nodo.Referidos.Add(hijoNodo);
                        }
                    }
                }

                return nodo;
            }

            return CrearNodo(usuarioRoot.UsuarioId, 0);
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
