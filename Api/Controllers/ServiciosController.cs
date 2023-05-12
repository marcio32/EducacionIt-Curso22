using Api.Services;
using Data.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : Controller
    {
        private readonly ServiciosServices _services;
        public ServiciosController()
        {
            _services = new ServiciosServices();
        }

        [HttpGet]
        [Route("BuscarServicios")]
        public async Task<List<Servicios>> BuscarServicios()
        {
            return await _services.BuscarLista();
        }

        [HttpPost]
        [Route("GuardarServicio")]
        public async Task<bool> GuardarUsuario(ServiciosDto servicioDto)
        {
            return await _services.Guardar(servicioDto);
        }

        [HttpPost]
        [Route("EliminarServicio")]
        public async Task<bool> EliminarUsuario(ServiciosDto servicioDto)
        {
            return await _services.Eliminar(servicioDto);
        }
    }
}
