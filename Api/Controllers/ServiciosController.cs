using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ServiciosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
