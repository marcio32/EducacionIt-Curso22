using Data.Dto;
using Data.Entities;
using Data.Managers;
using Web.Interfaces;

namespace Web.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly UsuariosManager _manager;

        public UsuariosService()
        {
            _manager = new UsuariosManager();
        }

        public async Task<Usuarios> buscarUsuario(LoginDto usuario)
        {

            return await _manager.BuscarUsuarioGoogleAsync(usuario);
        }
    }
}
