using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using webBotica2.Models;

namespace webBotica2.Controllers
{
    [Route("ClienteAPI")]
    public class ClienteAPI : Controller
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public const string API_KEY = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6Imp1YW5uZXZhZG8wNDE2QGdtYWlsLmNvbSJ9.UnsPBzFdeZ7KkvLESCPFJhiKunx2ntB5FB21HYgUISE";

        public ClienteAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("ConsultarNombrePorDni")]
        public async Task<string> ConsultarNombrePorDni(string dni)
        {
            var url = $"https://dniruc.apisperu.com/api/v1/dni/{dni}?token={API_KEY}";

            using var http = new HttpClient();
            var response = await http.GetStringAsync(url);

            var json = JObject.Parse(response);
            string nombreCompleto = json["nombres"]?.ToString() + " " + json["apellidoPaterno"]?.ToString() + " " + json["apellidoMaterno"]?.ToString();

            return nombreCompleto;
        }


        [HttpGet("ConsultarRazonSocialPorRuc")]
        public async Task<string> ConsultarRazonSocialPorRuc(string ruc)
        {
            var url = $"https://dniruc.apisperu.com/api/v1/ruc/{ruc}?token={API_KEY}";

            using var http = new HttpClient();
            var response = await http.GetStringAsync(url);

            var json = JObject.Parse(response);
            string razonSocial = json["razonSocial"]?.ToString();

            return razonSocial;
        }

       

    }
}
