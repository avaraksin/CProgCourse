﻿using GifSrvice.Data;
using Newtonsoft.Json;


namespace GifSrvice.BussinessLogik
{
    public class CurrencyRates
    {
        static string path = "https://openexchangerates.org/api/historical/";
        static string pathTool = ".json";
        static HttpClient client = new HttpClient();
        public static CurrencyRates instance;
            
        public static CurrencyRates GetInstance()
        {
            if (instance == null)
            {
                instance = new CurrencyRates();
            }
            return instance;
        }

        public CurReport GetRate(DateTime date)
        {
            Currency currency = new Currency();
            
            string fulpath = path + date.ToString("yyyy-MM-dd") + pathTool;
            
            fulpath += "?app_id=" + currency.app_id;
            fulpath += ";symbols=" + currency.symbols;
            fulpath += ";base=" + currency.Base;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fulpath);
            HttpResponseMessage response = client.Send(request);
            
            return JsonConvert.DeserializeObject<CurReport>(response.Content.ReadAsStringAsync().Result);
        }

        public int DynRates()
        {
            return GetRate(DateTime.Now).rates.RUB >= GetRate(DateTime.Now.AddDays(-1)).rates.RUB ? 1 : 0;
        }
    }
}