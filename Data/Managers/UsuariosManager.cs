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
    public class UsuariosManager : BaseManager<Usuarios>
    {
        public override async Task<List<Usuarios>> BuscarLista()
        {
            return await contextoSingleton.Usuarios.ToListAsync();
        }
    }
}
