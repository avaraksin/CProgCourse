using GifSrvice.Data;
using GifSrvice.Interface;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace GifSrvice.BussinessLogik
{
    public class Gif : IGif
    {
        private IHttpClientFactory _httpClientFactory;

        public Gif(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Gifdata?> GetImage(string? param)
        {
            var fullpath = $"https://api.giphy.com/v1/gifs/random";

            Dictionary<string, string?> uriDict = new()
            {
                { "api_key", @"GTRyejAYZqD0cfjcbjh74d8V6tfY0YEK" },
                { "tag",     param },
                { "rating",  "g" }
            };

            fullpath = QueryHelpers.AddQueryString(fullpath, uriDict);

            var client = _httpClientFactory.CreateClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullpath);
            HttpResponseMessage response = client.Send(request);

            return JsonConvert.DeserializeObject<Gifdata>(await response.Content.ReadAsStringAsync());
        }

        public string? GetGifUrl(Gifdata? gifdata)
        {
                return gifdata?.data?.images?.preview?.mp4;
        }
    }
}
