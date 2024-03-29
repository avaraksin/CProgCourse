﻿using GifSrvice.Controllers;
using GifSrvice.Data;
using GifSrvice.Interface;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;



namespace GifSrvice.BussinessLogik
{
    public class CurrencyRates: ICurrencyRates
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly Currency currency;

        public CurrencyRates(IHttpClientFactory clientFactory, IOptions<Currency> configuration)
        {
            _httpClientFactory = clientFactory;
            currency = configuration.Value;
        }

        public async Task<CurReport?> GetRate(DateTime date)
        {
            var fullpathUri = $"{currency.url}{date.ToString("yyyy-MM-dd")}{currency.pathTool}";

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
            
            return JsonConvert.DeserializeObject<CurReport?>(await response.Content.ReadAsStringAsync());
        }

        public async Task<int> IsCurrencyRiseFromYesterday()
        {
            return (await GetRate(DateTime.Now))?.rates?.value >= (await GetRate(DateTime.Now.AddDays(-1)))?.rates?.value ? 1 : 0;
        }
    }
}
