using Data.Dto;
using Data.Entities;

namespace Web.Interfaces
{
    public interface IRecuperarCuentaService
    {
        Task<Usuarios?> BuscarUsuario(string mail = null, int? codigo = 0);
        Task<bool> GuardarCodigo(Usuarios usuario);

    }
}
