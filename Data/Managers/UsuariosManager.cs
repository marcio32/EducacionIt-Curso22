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
            return await contextoSingleton.Usuarios.Where(x => x.Activo == true).Include(x=> x.Roles).ToListAsync();
        }

        public async Task<Usuarios> BuscarUsuario(string mail, string clave)
        {
            return await contextoSingleton.Usuarios.Where(x => x.Activo == true && x.Mail == mail && x.Clave == clave).Include(x=> x.Roles).FirstOrDefaultAsync();
        }

        public override async Task<bool> Eliminar(Usuarios entidad)
        {
            contextoSingleton.Entry(entidad).State = EntityState.Modified;

            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(entidad).State = EntityState.Detached;
            return resultado;
        }
    }
}
