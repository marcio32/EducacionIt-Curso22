using Data.Dto;
using Data.Entities;

namespace Web.ViewModels
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator RolesViewModel(RolesDto rolDto)
        {
            var rol = new RolesViewModel();
            rol.Id = rolDto.Id;
            rol.Nombre = rolDto.Nombre;
            rol.Activo = rolDto.Activo;
            return rol;
        }
    }
}
