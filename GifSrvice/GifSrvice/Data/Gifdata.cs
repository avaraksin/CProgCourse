using Newtonsoft.Json;

namespace GifSrvice.Data
{
    public class Gifdata
    {
        [JsonProperty("data")]
        public Data data;
    }

    public class Data
    {
        [JsonProperty("type")]
        public string type;
        [JsonProperty("id")]
        public string id;
        [JsonProperty("url")]
        public string url;

        [JsonProperty("images")]
        public Images images;
    }

    
    public class Images
    {
        [JsonProperty("downsized_medium")]
        public Preview preview;
    }

    public class Preview
    {
        [JsonProperty("url")]
        public string mp4;
    }
}
