using Data.Entities;
using Data.Managers;

namespace Api.Services
{
    public class UsuariosServices
    {
        private readonly UsuariosManager _manager;
        public UsuariosServices()
        {
            _manager = new UsuariosManager();
        }

        public async Task<List<Usuarios>> BuscarLista()
        {
            var buscarLista = await _manager.BuscarLista();
            return buscarLista;
        }
    }
}
