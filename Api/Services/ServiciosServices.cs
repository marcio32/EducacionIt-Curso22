using Data.Dto;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Identity;

namespace Api.Services
{
    public class ServiciosServices
    {
        private readonly ServiciosManager _manager;
        public ServiciosServices()
        {
            _manager = new ServiciosManager();
        }

        public async Task<List<Servicios>> BuscarLista()
        {
            var buscarServicios = await _manager.BuscarLista();
            return buscarServicios;
        }
        public async Task<bool> Guardar(ServiciosDto servicioDto)
        {
            var servicio = new Servicios();
            servicio = servicioDto;
            return await _manager.Guardar(servicio);
        }

        public async Task<bool> Eliminar(ServiciosDto servicioDto)
        {
            var servicio = new Servicios();
            servicio = servicioDto;
            return await _manager.Eliminar(servicio);
        }
    }
}
