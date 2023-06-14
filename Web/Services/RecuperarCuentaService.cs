using Data.Entities;
using Data.Managers;
using Web.Interfaces;

namespace Web.Services
{
    public class RecuperarCuentaService : IRecuperarCuentaService
    {
        private readonly RecuperarCuentaManager _manager;

        public RecuperarCuentaService()
        {
			_manager = new RecuperarCuentaManager();

		}

        public async Task<Usuarios?> BuscarUsuario(string mail = null, int? codigo = 0)
        {
            return await _manager.BuscarLista(mail, codigo);
        }

        public async Task<bool> GuardarCodigo(Usuarios usuario)
        {
            return await _manager.Guardar(usuario, usuario.Id);
        }
    }
}
