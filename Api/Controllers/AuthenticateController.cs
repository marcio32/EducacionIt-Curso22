using Api.Services;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Commons.Helpers;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
		private readonly IConfiguration _config;
		private readonly UsuariosServices _usuariosServices;

        public AuthenticateController(IConfiguration config)
        {
            _usuariosServices = new UsuariosServices();
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            loginDto.Clave = EncryptHelper.Encriptar(loginDto.Clave);
            var validarUsuario = await _usuariosServices.BuscarUsuario(loginDto.Mail, loginDto.Clave);

            if(validarUsuario != null)
            {
                var claims = new List<Claim> 
                { 
                    new Claim(ClaimTypes.Email, validarUsuario.Mail),
                    new Claim(ClaimTypes.DateOfBirth, validarUsuario.Fecha_Nacimiento.ToString()),
                    new Claim(ClaimTypes.Role, validarUsuario.Roles.Nombre)
                };
                var token = CrearToken(claims);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString());
            }
            else
            {
                return Unauthorized();
            }


		}

        private JwtSecurityToken CrearToken(List<Claim> datos)
        {
            var firma =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Firma"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(24),
                claims: datos,
                signingCredentials: new SigningCredentials(firma, SecurityAlgorithms.HmacSha256)
				);
            return token;
        }
    }
}
