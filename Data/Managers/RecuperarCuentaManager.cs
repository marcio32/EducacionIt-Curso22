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
	public class RecuperarCuentaManager : BaseManager<Usuarios>
	{
		public async Task<Usuarios?> BuscarLista(string mail, int? codigo = 0)
		{
			if (codigo != 0)
			{
				return await contextoSingleton.Usuarios.Where(x => x.Codigo == codigo).FirstOrDefaultAsync();
			}
			else{
				return await contextoSingleton.Usuarios.Where(x => x.Mail == mail).FirstOrDefaultAsync();
			}
			
		}

		public override Task<List<Usuarios>> BuscarLista()
		{
			throw new NotImplementedException();
		}

		public override Task<bool> Eliminar(Usuarios entidad)
		{
			throw new NotImplementedException();
		}
	}
}
