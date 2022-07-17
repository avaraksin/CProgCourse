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
        
        // GET: api/image
        [HttpGet]
        [Route("image")]

        public ContentResult GetImage()
        {
            string image = CurrencyRates.GetInstance().DynRates() == 1 ?
                Gif.GetInstance().GetGifUrl(Gif.GetInstance().GetImage("money")) : 
                Gif.GetInstance().GetGifUrl(Gif.GetInstance().GetImage("no money"));

            return new ContentResult
            {
                ContentType = "text/html",
                Content = $"<img src = \"" + image + "\">\n"
            };
                        
        }
    }
}
