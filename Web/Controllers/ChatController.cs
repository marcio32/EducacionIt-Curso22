using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	[Authorize]
	public class ChatController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}
