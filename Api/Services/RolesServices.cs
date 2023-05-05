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

        public async Task<List<Roles>> BuscarRoles()
        {
            var buscarRoles = await _manager.BuscarLista();
            return buscarRoles;
        }
    }
}
