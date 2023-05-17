using Api.Services;
using Data.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	
	[ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly RolesServices _services;
        public RolesController()
        {
            _services = new RolesServices();
        }

        [HttpGet]
        [Route("BuscarRoles")]
        public async Task<List<Roles>> BuscarRoles()
        {
            return await _services.BuscarLista();
        }

		[Authorize]
		[HttpPost]
        [Route("GuardarRol")]
        public async Task<bool> GuardarUsuario(RolesDto rolDto)
        {
            return await _services.Guardar(rolDto);
        }

		[Authorize]
		[HttpPost]
        [Route("EliminarRol")]
        public async Task<bool> EliminarUsuario(RolesDto rolDto)
        {
            return await _services.Eliminar(rolDto);
        }
    }
}
