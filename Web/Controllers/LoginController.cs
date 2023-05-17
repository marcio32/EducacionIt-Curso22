using Data.Base;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpClientFactory _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Ingresar(LoginDto loginDto)
        {
            var baseApi = new BaseApi(_httpClient);
            var login = await baseApi.PostToApi("Authenticate/Login", loginDto);
            var resultadoLogin = login as OkObjectResult;

            if(resultadoLogin == null) {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public async Task<IActionResult> CerrarSesion()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}
