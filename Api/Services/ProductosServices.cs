using Api.Interfaces;
using Data.Dto;
using Data.Entities;
using Data.Managers;

namespace Api.Services
{
    public class ProductosServices : IProductosService
    {
        private readonly ProductosManager _manager;
        public ProductosServices()
        {
            _manager = new ProductosManager();
        }

        public async Task<List<Productos>> BuscarLista()
        {
            var buscarLista = await _manager.BuscarLista();
            return buscarLista;
        }

        public async Task<bool> Guardar(ProductosDto productosDto)
        {
            var producto = new Productos();
            producto = productosDto;
            return await _manager.Guardar(producto, producto.Id);
        }

        public async Task<bool> Eliminar(ProductosDto productosDto)
        {
            var producto = new Productos();
            producto = productosDto;
            producto.Activo = false;
            return await _manager.Eliminar(producto);
        }
    }
}
