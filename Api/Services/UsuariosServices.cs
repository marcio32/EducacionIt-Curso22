using Commons.Helpers;
using Data.Dto;
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
            //foreach (var x in buscarLista)
            //{
            //    x.Clave = EncryptHelper.Desencriptar(x.Clave);
            //}
            return buscarLista;
        }

		public async Task<Usuarios> BuscarUsuario(string mail)
		{
			return await _manager.BuscarUsuario(mail);
		}

		public async Task<Usuarios> BuscarUsuario(string mail, string clave)
        {
            return await _manager.BuscarUsuario(mail, clave);
        }

        public async Task<bool> Guardar(UsuariosDto usuariosDto)
        {

			var buscarUsuario = _manager.BuscarUsuarioRepetido(usuariosDto.Mail);
            if(buscarUsuario.Result != null && usuariosDto.Id == 0)
            {
                return false;
            }
			var usuario = new Usuarios();
            usuario = usuariosDto;
			usuario.Clave = EncryptHelper.Encriptar(usuario.Clave);
            return await _manager.Guardar(usuario, usuario.Id);
        }

        public async Task<bool> Eliminar(UsuariosDto usuariosDto)
        {
            var usuario = new Usuarios();
            usuario = usuariosDto;
            usuario.Activo = false;
            return await _manager.Eliminar(usuario);
        }
    }
}
