using Data.Dto;

namespace Web.ViewModels
{
    public class ProductosViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? Imagen { get; set; }
        public IFormFile? Imagen_Archivo { get; set; }
        public bool Activo { get; set; }

        public static implicit operator ProductosViewModel(ProductosDto productoDto)
        {
            var productoViewModel = new ProductosViewModel();
            productoViewModel.Id = productoDto.Id;
            productoViewModel.Descripcion = productoDto.Descripcion;
            productoViewModel.Precio = productoDto.Precio;
            productoViewModel.Stock = productoDto.Stock;
            productoViewModel.Imagen = productoDto.Imagen;
            productoViewModel.Activo = productoDto.Activo;
            return productoViewModel;
        }
    }
}
