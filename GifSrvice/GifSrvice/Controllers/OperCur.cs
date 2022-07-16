using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GifSrvice.Data;
using GifSrvice.BussinessLogik;

// JsonConvert.SerializeObject

namespace GifSrvice.Controllers
{
    [Route("api")]
    [ApiController]
    public class OperCur : ControllerBase
    {
        // GET: api/rate
        [HttpGet]
        [Route("rate")]
        public string GetRate()
        {
            return CurrencyRates.GetInstance().DynRates().ToString();            
        }        
    }
}
