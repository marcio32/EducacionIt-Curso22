using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ServiciosController : Controller
    {
        public IActionResult Servicios()
        {
            return View();
        }
    }
}
