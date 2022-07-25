using Newtonsoft.Json;

namespace GifSrvice.Data
{
    public class Gifdata
    {
        [JsonProperty("data")]
        public Data? data;
    }

    public class Data
    {
        [JsonProperty("type")]
        public string? type;
        [JsonProperty("id")]
        public string? id;
        [JsonProperty("url")]
        public string? url;

        [JsonProperty("images")]
        public Images? images;

        [JsonProperty("something")]
        public string? something;
    }

    
    public class Images
    {
        [JsonProperty("downsized_medium")]
        public Preview? preview;
    }

    public class Preview
    {
        [JsonProperty("url")]
        public string? mp4;
    }

    public class GifServiceSettings
    {
        [JsonProperty("URL")]
        public string? url { get; set; }
        [JsonProperty("api_key")]
        public string? api_key { get; set; }
        [JsonProperty("rating")]
        public string rating { get; set; }
    }
}
