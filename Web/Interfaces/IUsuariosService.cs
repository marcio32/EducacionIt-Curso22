using Data.Dto;
using Data.Entities;

namespace Web.Interfaces
{
    public interface IUsuariosService
    {
        Task<Usuarios> buscarUsuario(LoginDto usuario);
    }
}
