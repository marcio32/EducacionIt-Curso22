using Data.Base;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public RolesController(IHttpClientFactory httpclient)
        {
            _httpClient = httpclient;
        }
        public IActionResult Roles()
        {
            return View();
        }

        public async Task<IActionResult> RolesAddPartial([FromBody] RolesDto rolDto)
        {
            var rolViewModel = new RolesViewModel();

            if (rolDto != null)
            {
                rolViewModel = rolDto;
            }

            return PartialView("~/Views/Roles/Partial/RolesAddPartial.cshtml", rolViewModel);
        }

        public IActionResult GuardarRol(RolesDto rolDto)
        {
            var baseApi = new BaseApi(_httpClient);
            baseApi.PostToApi("Roles/GuardarRol", rolDto);
            return View("~/Views/Roles/Roles.cshtml");
        }

        public IActionResult EliminarRol([FromBody] RolesDto rolDto)
        {
            var baseApi = new BaseApi(_httpClient);
            baseApi.PostToApi("Roles/EliminarRol", rolDto);
            return View("~/Views/Roles/Roles.cshtml");
        }
    }
}
