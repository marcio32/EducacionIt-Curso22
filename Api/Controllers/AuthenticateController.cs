using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AuthenticateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
