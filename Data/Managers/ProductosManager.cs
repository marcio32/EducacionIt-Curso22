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
    public class ProductosManager : BaseManager<Productos>
    {
        public override async Task<List<Productos>> BuscarLista()
        {
            return await contextoSingleton.Productos.Where(x => x.Activo == true).ToListAsync();
        }

        public override async Task<bool> Eliminar(Productos entidad)
        {
            contextoSingleton.Entry(entidad).State = EntityState.Modified;

            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(entidad).State = EntityState.Detached;
            return resultado;
        }
    }
}
