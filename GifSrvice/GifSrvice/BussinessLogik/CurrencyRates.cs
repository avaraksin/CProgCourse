using GifSrvice.Controllers;
using GifSrvice.Data;
using Newtonsoft.Json;


namespace GifSrvice.BussinessLogik
{
    public class CurrencyRates: ICurrencyRates
    {
        private IHttpClientFactory _httpClientFactory;

        static string path = $"https://openexchangerates.org/api/historical/";
        static string pathTool = ".json";

        public static CurrencyRates instance;

            
        public CurrencyRates(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public CurReport GetRate(DateTime date)
        {
            Currency currency = new Currency();
            
            string fulpath = path + date.ToString("yyyy-MM-dd") + pathTool;
            
            fulpath += "?app_id=" + currency.app_id;
            fulpath += ";symbols=" + currency.symbols;
            fulpath += ";base=" + currency.Base;

            //var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var client = _httpClientFactory.CreateClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fulpath);
            HttpResponseMessage response = client.Send(request);
            
            return JsonConvert.DeserializeObject<CurReport>(response.Content.ReadAsStringAsync().Result);
        }

        public int DynRates()
        {
            return GetRate(DateTime.Now).rates.value >= GetRate(DateTime.Now.AddDays(-1)).rates.value ? 1 : 0;
        }
    }
}
