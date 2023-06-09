using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Productos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? Imagen { get; set; }
        public bool Activo { get; set; }

        public static implicit operator Productos(ProductosDto productosDto)
        {
            var producto = new Productos();
            producto.Id = productosDto.Id;
            producto.Descripcion = productosDto.Descripcion;
            producto.Precio = productosDto.Precio;
            producto.Stock = productosDto.Stock;
            producto.Imagen = productosDto.Imagen;
            producto.Activo = productosDto.Activo;
            return producto;
        }
    }
}
