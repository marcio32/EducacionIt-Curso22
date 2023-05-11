using Data.Dto;
using Data.Entities;
using Data.Managers;
using Microsoft.AspNetCore.Identity;

namespace Api.Services
{
    public class RolesServices
    {
        private readonly RolesManager _manager;
        public RolesServices()
        {
            _manager = new RolesManager();
        }

        public async Task<List<Roles>> BuscarLista()
        {
            var buscarRoles = await _manager.BuscarLista();
            return buscarRoles;
        }
        public async Task<bool> Guardar(RolesDto rolDto)
        {
            var rol = new Roles();
            rol = rolDto;
            return await _manager.Guardar(rol, rol.Id) ;
        }

        public async Task<bool> Eliminar(RolesDto rolDto)
        {
            var rol = new Roles();
            rol = rolDto;
            rol.Activo = false;
            return await _manager.Eliminar(rol);
        }
    }
}
