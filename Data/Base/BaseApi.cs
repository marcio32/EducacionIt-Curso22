using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Data.Base
{
    public class BaseApi : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BaseApi(IHttpClientFactory httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<IActionResult> PostToApi(string ControllerName, object model, string token = "")
        {
            var client = _httpClient.CreateClient("useApi");

            if(token != "")
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

            var response = await client.PostAsJsonAsync(ControllerName, model);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return BadRequest();
        }

        public async Task<IActionResult> GetToApi(string ControllerName, string token = "")
        {
            var client = _httpClient.CreateClient("useApi");

            if (token != "")
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

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
