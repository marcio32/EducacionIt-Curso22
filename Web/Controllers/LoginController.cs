using Api.Controllers;
using Commons.Helpers;
using Data.Base;
using Data.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
    {

        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
		private readonly SmtpClient _smtpClient;

        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory;
            _configuration = configuration;
			_smtpClient = new SmtpClient();
        }

		#region Views
		public IActionResult Login()
        {
            if (TempData["ErrorLogin"] != null)
            {
                ViewBag.ErrorLogin = TempData["ErrorLogin"].ToString();

			}
            return View();
        }

		public IActionResult OlvidoClave()
		{
			return View("~/Views/Login/OlvidoClave.cshtml");
		}

		public IActionResult RecuperarCuenta()
		{
			return View("~/Views/Login/RecuperarCuenta.cshtml");
		}

		public IActionResult CrearCuenta()
		{
			return View("~/Views/Login/CrearCuenta.cshtml");
		}
		#endregion

		#region Funciones
		public async Task<IActionResult> Ingresar(LoginDto loginDto)
        {
         
				var baseApi = new BaseApi(_httpClient);
				var login = await baseApi.PostToApi("Authenticate/Login", loginDto);
				var resultadoLogin = login as OkObjectResult;

				if (resultadoLogin != null)
				{
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
					//ViewBag.ErrorLogin = "La contraseña o el mail no coinciden";
					TempData["ErrorLogin"] = "La contraseña o el mail no coinciden";
					return RedirectToAction("Login", "Login");
				}
			
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

		public async Task<IActionResult> EnviarMail(LoginDto login)
		{
			var guid = Guid.NewGuid();
			var numeros = new String(guid.ToString().Where(Char.IsDigit).ToArray());
			var seed = int.Parse(numeros.Substring(0, 6));
			var random = new Random(seed);
			var codigo = random.Next(000000, 999999);

			var recuperarCuentaService = new RecuperarCuentaService();
			var usuario = recuperarCuentaService.BuscarUsuario(login.Mail);
			if(usuario.Result == null)
			{
				return RedirectToAction("OlvidoClave", "Login");
			}
			usuario.Result.Codigo = codigo;
			var resultadoLogin = recuperarCuentaService.GuardarCodigo(usuario.Result);

			if(resultadoLogin.Result == true)
			{
				MailMessage mail = new();
				string cuerpoMail = CuerpoMail(codigo);
				mail.From = new MailAddress(_configuration["ConfiguracionMail:Usuario"]);
				mail.To.Add(login.Mail);
				mail.Subject = "Codigo de Recuperacion";
				mail.Body = cuerpoMail;
				mail.IsBodyHtml = true;
				mail.Priority = MailPriority.Normal;

				_smtpClient.Host = _configuration["ConfiguracionMail:DireccionServidor"];
				_smtpClient.Port = int.Parse(_configuration["ConfiguracionMail:Puerto"]);
				_smtpClient.EnableSsl = bool.Parse(_configuration["ConfiguracionMail:Ssl"]);
				_smtpClient.UseDefaultCredentials = false;
				_smtpClient.Credentials = new NetworkCredential(_configuration["ConfiguracionMail:Usuario"], _configuration["ConfiguracionMail:Clave"]);
				_smtpClient.Send(mail);
				return RedirectToAction("RecuperarCuenta", "Login");
			}
			else
			{
				return RedirectToAction("Login", "Login");
			}
		}

		public async  Task<IActionResult> CambiarClave(LoginDto login)
		{
			var recuperarCuentaService = new RecuperarCuentaService();
			var usuario = recuperarCuentaService.BuscarUsuario(codigo:login.Codigo);
			if(usuario.Result == null)
			{
				TempData["ErrorLogin"] = "El codigo ingresado no coincide con el enviado al mail, por favor genere uno nuevo";
				return RedirectToAction("Login", "Login");
			}
			usuario.Result.Clave = EncryptHelper.Encriptar(login.Clave);
			usuario.Result.Codigo = null;
			var resultadoLogin = recuperarCuentaService.GuardarCodigo(usuario.Result);
			if(resultadoLogin.Result == true) {
				TempData["ErrorLogin"] = "Se cambio correctamente la clave";
				return RedirectToAction("Login", "Login");
			}
			else
			{
				TempData["ErrorLogin"] = "Error, por favor contacte a sistemas";
				return RedirectToAction("Login", "Login");
			}
		}

		private static string CuerpoMail(int codigo)
		{
			var separacion = "<br>";
			var mensaje = "<h3><strong>A continacion se mostrara un codigo que debera ingresar en la Web del curso de Educacion It </strong></h3>"; 
			mensaje += $"<strong>{codigo}</strong> {separacion}";
			return mensaje;
		}

		public async void LoginGoogle()
		{
			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
			{
				RedirectUri = Url.Action("GoogleResponse")
			}) ;
		}

		public async Task<IActionResult> GoogleResponse()
		{
			var resultado = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			var claims = resultado.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
			{
				claim.Value,
				claim.Type,
				claim.Issuer,
				claim.OriginalIssuer
			});

			var login = new LoginDto();
			login.Mail = claims.ToList()[4].Value;
			var usuariosService = new UsuariosService();
			var usuario = usuariosService.buscarUsuario(login).Result;

			if(usuario != null)
			{
				var authenticate = new AuthenticateController(_configuration);
				var token = await authenticate.LoginGoogle(login);
				var resultadoToken = token as OkObjectResult;

				var resultadoSplit = resultadoToken.Value.ToString().Split(';');
				HttpContext.Session.SetString("Token", resultadoSplit[0]);
				var homeViewModel = new HomeViewModel();
				homeViewModel.Token = resultadoSplit[0];
				homeViewModel.AjaxUrl = _configuration["ServiceUrl:AjaxUrl"];
				return View("~/Views/Home/Index.cshtml", homeViewModel);
			}
			else
			{
				TempData["ErrorLogin"] = "El usuario no existe";
				return RedirectToAction("Login", "Login");
			}
		}

		public async Task<IActionResult> CrearUsuario(CrearUsuarioDto usuario)
		{
			var baseApi = new BaseApi(_httpClient);
			var resultado = await baseApi.PostToApi("Usuarios/CrearUsuario", usuario, "");
			var resultadoCrearUsuario = resultado as OkObjectResult;

			if(resultadoCrearUsuario != null && resultadoCrearUsuario.Value.ToString() == "true")
			{
				TempData["ErrorLogin"] = "Se ha creado su cuenta correctamente";
				return RedirectToAction("Login", "Login");
			}
			else
			{
				TempData["ErrorLogin"] = "No se ha podido crear la cuenta. Contacte con sistemas";
				return RedirectToAction("Login", "Login");
			}
			return null;
		}

		#endregion
	}
}
