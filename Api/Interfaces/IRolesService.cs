using Data.Dto;
using Data.Entities;

namespace Api.Interfaces
{
    public interface IRolesService
    {
        Task<List<Roles>> BuscarLista();
        Task<bool> Guardar(RolesDto rolDto);
        Task<bool> Eliminar(RolesDto rolDto);
    }
}
