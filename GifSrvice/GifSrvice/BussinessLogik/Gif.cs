using GifSrvice.Data;
using GifSrvice.Interface;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace GifSrvice.BussinessLogik
{
    public class Gif : IGif
    {
        private IHttpClientFactory _httpClientFactory;

        private readonly GifServiceSettings settings;

        public Gif(IHttpClientFactory httpClientFactory, IOptions<GifServiceSettings> configuration)
        {
            _httpClientFactory = httpClientFactory;
            settings = configuration.Value;
        }

        public async Task<Gifdata?> GetImage(string? param)
        {
            var fullpath = settings.url;

            Dictionary<string, string?> uriDict = new()
            {
                { "api_key", settings.api_key   },
                { "tag",     param              },
                { "rating",  settings.rating    }
            };

            fullpath = QueryHelpers.AddQueryString(fullpath, uriDict);

            var client = _httpClientFactory.CreateClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullpath);
            HttpResponseMessage response = client.Send(request);

            return JsonConvert.DeserializeObject<Gifdata>(await response.Content.ReadAsStringAsync());
        }

        public string? GetGifUrl(Gifdata gifdata)
        {
            return gifdata?.data?.images?.preview?.mp4;
        }
    }
}
