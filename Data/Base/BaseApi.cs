using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    public class BaseApi : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BaseApi(IHttpClientFactory httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<IActionResult> PostToApi(string ControllerName, object model)
        {
            var client = _httpClient.CreateClient("useApi");
            var response = await client.PostAsJsonAsync(ControllerName, model);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return BadRequest();
        }

        public async Task<IActionResult> GetToApi(string ControllerName)
        {
            var client = _httpClient.CreateClient("useApi");
            var response = await client.GetAsync(ControllerName);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return BadRequest();
        }
    }
}
