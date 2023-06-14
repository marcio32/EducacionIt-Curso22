using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IServiciosServices
    {
        Task<List<Servicios>> BuscarLista();
        Task<bool> Guardar(ServiciosDto servicioDto);
        Task<bool> Eliminar(ServiciosDto servicioDto);
    }
}
