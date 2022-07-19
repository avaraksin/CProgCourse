using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GifSrvice.Data;
using GifSrvice.BussinessLogik;
using GifSrvice.Interface;

// JsonConvert.SerializeObject


namespace GifSrvice.Controllers
{
    [Route("api")]
    [ApiController]
    public class OperCurController : ControllerBase
    {
        private ICurrencyRates _currencyRates;
        private IGif _gif;

        private static int dayShift = -1;


        public OperCurController(IHttpClientFactory httpClientFactory)
        {
            _currencyRates = new CurrencyRates(httpClientFactory);
            _gif = new Gif(httpClientFactory);
        }

        // GET: api/rate

        [HttpGet]
        [Route("rate")]
        public string GetRate()
        {
            float nowRate = _currencyRates.GetRate(DateTime.Now).rates.value;
            float beforeRate  = _currencyRates.GetRate(DateTime.Now.AddDays(dayShift)).rates.value;
            
            string text = "Курс рубля к доллару на сегодня (" + DateTime.Now.ToString("dd.MM.yyyy") + ") = " +
                    nowRate.ToString();
            text += $"\nКурс рубля к доллару на " + DateTime.Now.AddDays(dayShift).ToString("dd.MM.yyyy") + " = " +
                    beforeRate.ToString();
            text += $"\nРазница = " + (nowRate - beforeRate).ToString() + $"\nФункция вернула ";
            return text + _currencyRates.DynRates().ToString();            
        }
        
        // GET: api/image
        [HttpGet]
        [Route("image")]
        public ContentResult GetImage()
        {
            string image = _currencyRates.DynRates() == 1 ?
                _gif.GetGifUrl(_gif.GetImage("money")) : 
                _gif.GetGifUrl(_gif.GetImage("no money"));

            return new ContentResult
            {
                ContentType = "text/html",
                Content = $"<img src = \"" + image + "\">\n"
            };
                        
        }
    }
}
