using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GifSrvice.Data;
using GifSrvice.BussinessLogik;

// JsonConvert.SerializeObject


namespace GifSrvice.Controllers
{
    public interface ICurrencyRates
    {
        CurReport GetRate(DateTime date);
        public int DynRates();
    }

    [Route("api")]
    [ApiController]
    public class OperCurController : ControllerBase
    {
        private ICurrencyRates _currencyRates;
        
        public OperCurController(ICurrencyRates currencyRates)
        {
            _currencyRates = currencyRates;
        }

        // GET: api/rate

        [HttpGet]
        [Route("rate")]
        public string GetRate()
        {
           
            float nowRate = _currencyRates.GetRate(DateTime.Now).rates.value;
            float beforeRate  = _currencyRates.GetRate(DateTime.Now.AddDays(-1)).rates.value;
            
            string text = "Курс рубля к доллару на сегодня (" + DateTime.Now.ToString("dd.MM.yyyy") + ") = " +
                    nowRate.ToString();
            text += $"\nКурс рубля к доллару на вчера (" + DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy") + ") = " +
                    nowRate.ToString();
            text += $"\nРазница = " + (nowRate - beforeRate).ToString() + $"\nФункция вернула ";
            return text + _currencyRates.DynRates().ToString();            
        }
        
        // GET: api/image
        [HttpGet]
        [Route("image")]
        public ContentResult GetImage()
        {
            string image = _currencyRates.DynRates() == 1 ?
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
