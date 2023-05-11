using Data.Base;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public ProductosController(IHttpClientFactory httpclient)
        {
            _httpClient = httpclient;
        }
        public IActionResult Productos()
        {
            return View();
        }

        public async Task<IActionResult> ProductosAddPartial([FromBody] ProductosDto productoDto)
        {
            var productoViewModel = new ProductosViewModel();

            if (productoDto != null)
            {
                productoViewModel = productoDto;
            }

            return PartialView("~/Views/Productos/Partial/ProductosAddPartial.cshtml", productoViewModel);
        }

        public IActionResult GuardarProducto(ProductosDto productoDto)
        {
            var baseApi = new BaseApi(_httpClient);
            baseApi.PostToApi("Productos/GuardarProducto", productoDto);
            return View("~/Views/Productos/Productos.cshtml");
        }

        public IActionResult EliminarProducto([FromBody] ProductosDto productoDto)
        {
            var baseApi = new BaseApi(_httpClient);
            baseApi.PostToApi("Productos/EliminarProducto", productoDto);
            return View("~/Views/Productos/Productos.cshtml");
        }
    }
}
