using Api.Services;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        private readonly UsuariosServices _usuariosServices;

        public AuthenticateController()
        {
            _usuariosServices = new UsuariosServices();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validarUsuario = await _usuariosServices.BuscarUsuario(loginDto.Mail, loginDto.Clave);
            return Ok(validarUsuario);
		}
    }
}
