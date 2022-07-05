using Microsoft.AspNetCore.Mvc;
using GifSrvice.Data;
using System.Text.Json;


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
        public Cur_Report GetRate()
        {
            Currency currency = new Currency();
            Cur_Report cur_Report = new Cur_Report();
            path += "?app_id=" + new Currency().app_id;
            path += ";symbols=" + currency.symbols;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path);
            
                        
            HttpResponseMessage response = client.Send(request);
            cur_Report = JsonSerializer.Deserialize<Cur_Report>(response.Content.ReadAsStringAsync().Result);
            return cur_Report;
            
            
        }

        [HttpGet]
        [Route("GetJson")]
        public Cur_Report GetFromJson()
        {
            Cur_Report? cur_Report = new Cur_Report();
            string Jsonstring = @"{""disclaimer"":""Usage subject to terms""}";
            cur_Report = JsonSerializer.Deserialize<Cur_Report>(Jsonstring);

            return cur_Report;

        }
        
    }
}
