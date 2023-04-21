using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion
    }
}
