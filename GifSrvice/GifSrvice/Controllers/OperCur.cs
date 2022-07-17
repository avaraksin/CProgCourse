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
            float nowRate = CurrencyRates.GetInstance().GetRate(DateTime.Now).rates.value;
            float beforeRate = nowRate = CurrencyRates.GetInstance().GetRate(DateTime.Now.AddDays(-1)).rates.value;
            
            string text = "Курс рубля к доллару на сегодня (" + DateTime.Now.ToString("dd.MM.yyyy") + ") = " +
                    nowRate.ToString();
            text += $"\nКурс рубля к доллару на вчера (" + DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy") + ") = " +
                    nowRate.ToString();
            text += $"\nРазница = " + (nowRate - beforeRate).ToString() + $"\nФункция вернула ";
            return text + CurrencyRates.GetInstance().DynRates().ToString();            
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
