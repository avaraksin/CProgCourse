using GifSrvice.Controllers;
using GifSrvice.Data;
using GifSrvice.Interface;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;


namespace GifSrvice.BussinessLogik
{
    public class CurrencyRates: ICurrencyRates
    {
        private IHttpClientFactory _httpClientFactory;

        static string exchangeRatesUrl = $"https://openexchangerates.org/api/historical/";
        static string pathTool = ".json";

        public CurrencyRates(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<CurReport> GetRate(DateTime date)
        {
            Currency currency = new Currency();

            string fullpathUri = $"{exchangeRatesUrl}{date.ToString("yyyy-MM-dd")}{pathTool}";

            Dictionary<string, string?> parameters = new ()
            {
                { "app_id",     currency.app_id     },
                { "symbols",    currency.symbols    },
                { "base",       currency.Base       }
            };
            
            fullpathUri = QueryHelpers.AddQueryString(fullpathUri, parameters);

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, fullpathUri);
            var response = client.Send(request);
            
            return JsonConvert.DeserializeObject<CurReport>(await response.Content.ReadAsStringAsync());
        }

        public int IsCurrencyRiseFromYesterday()
        {
            return GetRate(DateTime.Now).rates.value >= GetRate(DateTime.Now.AddDays(-1)).rates.value ? 1 : 0;
        }
    }
}
