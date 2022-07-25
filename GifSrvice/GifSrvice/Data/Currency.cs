using Newtonsoft.Json;

namespace GifSrvice.Data
{
    public class Currency
    {
        [JsonProperty("app_id")]
        public string? app_id { get; set; }
        [JsonProperty("Base")]
        public string? Base { get; set; }
        [JsonProperty("symbols")]
        public string? symbols { get; set; }
        [JsonProperty("URL")]
        public string? url { get; set; }
        [JsonProperty("PathTool")]
        public string? pathTool { get; set; }
    }

    public class CurReport
    {
        [JsonProperty("disclaimer")]
        public string? disclaimer;
        [JsonProperty("license")]
        public string? license;
        [JsonProperty("timestamp")]
        public string? timestamp;
        [JsonProperty("base")]
        public string? Base;
        [JsonProperty("rates")]
        public Rates? rates;
    }

    public class Rates
    {
        [JsonProperty("RUB")]
        public float? value;
    }
}
