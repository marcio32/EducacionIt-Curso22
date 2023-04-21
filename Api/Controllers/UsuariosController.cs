using Api.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
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
    }
}
