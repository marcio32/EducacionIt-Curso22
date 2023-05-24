using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Managers
{
    public class ServiciosManager : BaseManager<Servicios>
    {

        public override async Task<List<Servicios>> BuscarLista()
        {
           return contextoSingleton.Servicios.FromSqlRaw("ObtenerServicios").ToList();
        }

        public async Task<bool> Guardar(Servicios entidad)
        {
            return  Convert.ToBoolean(contextoSingleton.Database.ExecuteSqlRaw($"GuardaroActualizarServicios '{entidad.Id}', '{entidad.Nombre}', '{entidad.Activo}'"));
        }

        public override async Task<bool> Eliminar(Servicios entidad)
        {
            return Convert.ToBoolean(contextoSingleton.Database.ExecuteSqlRaw($"Eliminarservicio '{entidad.Id}'"));
        }
    }
}
