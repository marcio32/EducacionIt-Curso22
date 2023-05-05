using Data.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.ViewModels
{
    public class UsuariosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Mail { get; set; }
        public string Clave { get; set; }
        public int Id_Rol { get; set; }
        public int? Codigo { get; set; }
        public bool Activo { get; set; }

        public IEnumerable<SelectListItem> Lista_Roles { get; set; }

        public static implicit operator UsuariosViewModel(UsuariosDto usuarioDto)
        {
            var usuarioViewModel = new UsuariosViewModel();
            usuarioViewModel.Id =  usuarioDto.Id;
            usuarioViewModel.Nombre = usuarioDto.Nombre;
            usuarioViewModel.Apellido = usuarioDto.Apellido;
            usuarioViewModel.Fecha_Nacimiento = usuarioDto.Fecha_Nacimiento;
            usuarioViewModel.Mail = usuarioDto.Mail;
            usuarioViewModel.Id_Rol = usuarioDto.Id_Rol;
            usuarioViewModel.Clave = usuarioDto.Clave;
            usuarioViewModel.Activo = usuarioDto.Activo;
            return usuarioViewModel;
        }
    }
}
