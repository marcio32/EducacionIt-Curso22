using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Roles
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator Roles(RolesDto rolDto)
        {
            var rol = new Roles();
            rol.Id = rolDto.Id;
            rol.Nombre = rolDto.Nombre;
            rol.Activo = rolDto.Activo;
            return rol;
        }
    }

   
}
