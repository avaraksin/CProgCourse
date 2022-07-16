using Newtonsoft.Json;

namespace GifSrvice.Data
{
    public class Currency
    {
        public string app_id = "eac80a2c15f84765baa70420b28c6a88";
        public string? Base;
        public string? symbols;

        public Currency()
        {
            this.Base = "USD";
            this.symbols = "RUB";
        }
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
        public Rates? rates;
    }

    public class Rates
    {
        [JsonProperty("RUB")]
        public float RUB;
    }
}
