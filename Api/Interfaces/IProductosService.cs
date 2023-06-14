using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IProductosService
    {
        Task<List<Productos>> BuscarLista();
        Task<bool> Guardar(ProductosDto productosDto);
        Task<bool> Eliminar(ProductosDto productosDto);
    }
}
