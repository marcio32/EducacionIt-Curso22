using Api.Services;
using Data.Entities;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Commons.Helpers;

namespace Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UsuariosController : Controller
	{
		private readonly UsuariosServices _services;
		public UsuariosController()
		{
			_services = new UsuariosServices();
		}

		[HttpGet]
		[Route("BuscarUsuarios")]
		public async Task<List<Usuarios>> BuscarUsuarios()
		{

			return await _services.BuscarLista();
		}

		[HttpPost]
		[Route("GuardarUsuario")]
		public async Task<bool> GuardarUsuario(UsuariosDto usuarioDto)
		{
			return await _services.Guardar(usuarioDto);
		}

		[HttpPost]
		[Route("EliminarUsuario")]
		public async Task<bool> EliminarUsuario(UsuariosDto usuariosDto)
		{
			return await _services.Eliminar(usuariosDto);
		}
	}
}
