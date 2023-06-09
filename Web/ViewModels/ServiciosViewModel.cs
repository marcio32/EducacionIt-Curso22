using Data.Dto;
using Data.Entities;

namespace Web.ViewModels
{
    public class ServiciosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator ServiciosViewModel(ServiciosDto servicioDto)
        {
            var servicio = new ServiciosViewModel();
            servicio.Id = servicioDto.Id;
            servicio.Nombre = servicioDto.Nombre;
            servicio.Activo = servicioDto.Activo;
            return servicio;
        }
    }
}
