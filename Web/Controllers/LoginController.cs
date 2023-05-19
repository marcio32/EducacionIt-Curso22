using Data.Base;
using Data.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.ViewModels;

namespace Web.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
    {

        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory;
            _configuration = configuration;
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

            if(resultadoLogin != null) {
                var resultadoSplit = resultadoLogin.Value.ToString().Split(';');
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimNombre = new(ClaimTypes.Name, resultadoSplit[1]);
                Claim claimRole = new(ClaimTypes.Role, resultadoSplit[2]);
                Claim claimEmail = new(ClaimTypes.Email, resultadoSplit[3]);

                identity.AddClaim(claimNombre);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimEmail);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddHours(24)
                });

                HttpContext.Session.SetString("Token", resultadoSplit[0]);
                var homeViewModel = new HomeViewModel();
				homeViewModel.Token = resultadoSplit[0];
                homeViewModel.AjaxUrl = _configuration["ServiceUrl:AjaxUrl"];

				return View("~/Views/Home/Index.cshtml", homeViewModel);
				
            }
            else
            {
				return RedirectToAction("Login", "Login");
			}
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }


    }
}
