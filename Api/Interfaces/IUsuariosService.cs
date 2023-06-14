using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IUsuariosService
    {
        Task<List<Usuarios>> BuscarLista();
        Task<Usuarios> BuscarUsuario(string mail);
        Task<Usuarios> BuscarUsuario(string mail, string clave);
        Task<bool> Guardar(UsuariosDto usuarioDto);
        Task<bool> CrearUsuario(CrearUsuarioDto crearUsuarioDto);
        Task<bool> Eliminar(UsuariosDto usuariosDto);

    }
}
