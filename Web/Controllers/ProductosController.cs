using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Productos()
        {
            return View();
        }
    }
}
