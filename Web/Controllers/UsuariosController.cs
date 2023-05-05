using Data.Dto;
using Data.Base;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly IHttpClientFactory _httpClient;

        public UsuariosController(IHttpClientFactory httpclient)
        {
            _httpClient = httpclient;
        }

        public IActionResult Usuarios()
        {
            return View();
        }

        public async Task<IActionResult> UsuariosAddPartial([FromBody] UsuariosDto usuarioDto)
        {
            var usuariosViewModel = new UsuariosViewModel();
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.GetToApi("Roles/BuscarRoles");
            var resultadoRoles = roles as OkObjectResult;

            if(usuarioDto != null)
            {
                usuariosViewModel = usuarioDto;
            }

            if(resultadoRoles != null)
            {
                var listaRoles = JsonConvert.DeserializeObject<List<RolesDto>>(resultadoRoles.Value.ToString());
                var listaItemsRoles = new List<SelectListItem>();
                
                foreach(var item in listaRoles)
                {
                    listaItemsRoles.Add(new SelectListItem { Text = item.Nombre, Value = item.Id.ToString() });
                }
                usuariosViewModel.Lista_Roles = listaItemsRoles;

            }

            return PartialView("~/Views/Usuarios/Partial/UsuariosAddPartial.cshtml", usuariosViewModel);
        }

        public IActionResult GuardarUsuario(UsuariosDto usuarioDto)
        {
            var baseApi = new BaseApi(_httpClient);
            baseApi.PostToApi("Usuarios/GuardarUsuario", usuarioDto);
            return View("~/Views/Usuarios/Usuarios.cshtml");
        }
    }
}
