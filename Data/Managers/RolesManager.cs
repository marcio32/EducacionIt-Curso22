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

        public override Task<bool> Eliminar(Roles entidad)
        {
            throw new NotImplementedException();
        }
    }
}
