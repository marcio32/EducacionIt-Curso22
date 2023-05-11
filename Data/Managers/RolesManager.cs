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
    public class RolesManager : BaseManager<Roles>
    {
       
        public override async Task<List<Roles>> BuscarLista()
        {
            return await contextoSingleton.Roles.Where(x => x.Activo == true).ToListAsync();
        }

        public override async Task<bool> Eliminar(Roles entidad)
        {
            contextoSingleton.Entry(entidad).State = EntityState.Modified;

            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(entidad).State = EntityState.Detached;
            return resultado;
        }
    }
}
