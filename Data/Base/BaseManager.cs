using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Base
{
    public abstract class BaseManager<T> where T : class
    {
        #region Singleton Context
        protected static ApplicationDbContext _context;

        public static ApplicationDbContext contextoSingleton
        {
            get
            {
                if (_context == null)
                {
                    _context = new ApplicationDbContext();
                }
                return _context;
            }
        }
        #endregion


        #region metodos Abstractos
        public abstract Task<List<T>> BuscarLista();
        public abstract Task<bool> Eliminar(T entidad);
        #endregion

        #region Metodos Publicos
        public async Task<bool> Guardar(T entidad, int id)
        {
            if (id == 0)
                contextoSingleton.Entry(entidad).State = EntityState.Added;
            else
                contextoSingleton.Entry(entidad).State = EntityState.Modified;

            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(entidad).State = EntityState.Detached;
            return resultado;
        }
        #endregion
    }
}
