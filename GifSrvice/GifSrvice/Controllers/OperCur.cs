using Microsoft.AspNetCore.Mvc;
using GifSrvice.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GifSrvice.Controllers
{
    [Route("api")]
    [ApiController]
    public class OperCur : ControllerBase
    {
        static string path = "https://openexchangerates.org/api/latest.json";
        static HttpClient client = new HttpClient();
        // GET: api/rate
        [HttpGet]
        [Route("rate")]
        public string GetRate()
        {
            Cur_Report product = new Cur_Report();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path);
            
            Currency currency = new Currency();

            
            
            
            HttpResponseMessage response = client.Send(request);
            if (response.IsSuccessStatusCode)
            {
            }
            return response.Content.ToString();
        }

        
    }
}
