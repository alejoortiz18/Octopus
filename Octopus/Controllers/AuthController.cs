using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Octopus.Controllers
{
    public class AuthController : Controller
    {
        
        public IActionResult InicioSesion()
        {
            
            return View();
        }
    }
}
