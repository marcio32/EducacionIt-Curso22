using Data.Base;
using Data.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
	[Authorize]
	public class ServiciosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public ServiciosController(IHttpClientFactory httpclient)
        {
            _httpClient = httpclient;
        }
        public IActionResult Servicios()
        {
            return View();
        }

        public async Task<IActionResult> ServiciosAddPartial([FromBody] ServiciosDto servicioDto)
        {
            var servicioViewModel = new ServiciosViewModel();

            if (servicioDto != null)
            {
                servicioViewModel = servicioDto;
            }

            return PartialView("~/Views/Servicios/Partial/ServiciosAddPartial.cshtml", servicioViewModel);
        }

        public IActionResult GuardarServicio(ServiciosDto servicioDto)
        {
            var baseApi = new BaseApi(_httpClient);

            var token = HttpContext.Session.GetString("Token");
            baseApi.PostToApi("Servicios/GuardarServicio", servicioDto, token);
            return View("~/Views/Servicios/Servicios.cshtml");
        }

        public IActionResult EliminarServicio([FromBody] ServiciosDto servicioDto)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = HttpContext.Session.GetString("Token");
            baseApi.PostToApi("Servicios/EliminarServicio", servicioDto, token);
            return View("~/Views/Servicios/Servicios.cshtml");
        }
    }
}
