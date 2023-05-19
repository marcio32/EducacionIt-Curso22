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

            if(productoDto.Imagen_Archivo != null && productoDto.Imagen_Archivo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    productoDto.Imagen_Archivo.CopyTo(ms);
                    var imagenBytes = ms.ToArray();
                    productoDto.Imagen = Convert.ToBase64String(imagenBytes);
                }
            }
            productoDto.Imagen_Archivo = null;
            var token = HttpContext.Session.GetString("Token");

            baseApi.PostToApi("Productos/GuardarProducto", productoDto, token);
            return View("~/Views/Productos/Productos.cshtml");
        }

        public IActionResult EliminarProducto([FromBody] ProductosDto productoDto)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = HttpContext.Session.GetString("Token");
            baseApi.PostToApi("Productos/EliminarProducto", productoDto, token);
            return View("~/Views/Productos/Productos.cshtml");
        }
    }
}
