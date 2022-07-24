using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GifSrvice.Data;
using GifSrvice.BussinessLogik;
using GifSrvice.Interface;
using System.Text;

// JsonConvert.SerializeObject


namespace GifSrvice.Controllers
{
    [Route("api")]
    [ApiController]
    public class OperCurController : ControllerBase
    {
        private ICurrencyRates _currencyRates;
        private IGif? _gif;
        private IHttpClientFactory _httpClientFactory;

        private static int dayShift = -1;

        private HttpContext? ctx;


        public OperCurController(IGif gif, ICurrencyRates currencyRates, IHttpClientFactory httpClientFactory, IHttpContextAccessor cx)
        {
            _currencyRates = currencyRates;
            _gif = gif;
            _httpClientFactory = httpClientFactory;
            ctx = cx.HttpContext;
        }

        // GET: api/rate

        [HttpGet]
        [Route("rate")]
        public async Task<string> GetRate()
        {
            float? nowRate = (await _currencyRates.GetRate(DateTime.Now))?.rates?.value;
            float? beforeRate = (await _currencyRates.GetRate(DateTime.Now.AddDays(dayShift)))?.rates?.value;

            var text = $"Курс рубля к доллару на сегодня ({DateTime.Now.ToString("dd.MM.yyyy")}) = {nowRate:F2}";
            text += $"\nКурс рубля к доллару на {DateTime.Now.AddDays(dayShift).ToString("dd.MM.yyyy")} = {beforeRate:F2}";
            text += $"\nРазница = {(nowRate - beforeRate):F2}";
            text += $"\nФункция вернула {await _currencyRates.IsCurrencyRiseFromYesterday()}";
            return text;            
        }
        
        // GET: api/image
        [HttpGet]
        [Route("image")]
        public async Task<IActionResult> GetImage()
        {
            var imageSearchName = (await _currencyRates.IsCurrencyRiseFromYesterday()) == 1 ? "rich" : "broke";

            var image = _gif?.GetGifUrl(await _gif?.GetImage(imageSearchName));
            
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, image);
            var response = client.Send(request);
            
            var buffer = await response.Content.ReadAsByteArrayAsync();

            return File(buffer, "image/gif");
        }

        [HttpGet]
        [Route("testbody")]
        public void TestBody()
        {
            ctx.Response.StatusCode = 200;
            //Set Content Type
            ctx.Response.ContentType = "plain/text";
            //Create Response
            ctx.Response.Headers.Add("SomeHeader", "Value");
            byte[] content = Encoding.ASCII.GetBytes($"This id BODY");
            //Send it to the Client
            ctx.Response.Body.WriteAsync(content, 0, content.Length);
           

            return;
        }
    }

    
}
