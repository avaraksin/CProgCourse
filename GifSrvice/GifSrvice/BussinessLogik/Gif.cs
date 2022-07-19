using GifSrvice.Data;
using GifSrvice.Interface;
using Newtonsoft.Json;

namespace GifSrvice.BussinessLogik
{
    public class Gif : IGif
    {
        private IHttpClientFactory _httpClientFactory;

        public Gif(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public Gifdata GetImage(string param)
        {
            string fulpath;

            fulpath = $"https://api.giphy.com/v1/gifs/random?api_key=GTRyejAYZqD0cfjcbjh74d8V6tfY0YEK&tag=" + param + $"&rating=g";

            var client = _httpClientFactory.CreateClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fulpath);
            HttpResponseMessage response = client.Send(request);

            return JsonConvert.DeserializeObject<Gifdata>(response.Content.ReadAsStringAsync().Result);
        }

        public string GetGifUrl(Gifdata gifdata)
        {
                return gifdata?.data?.images?.preview?.mp4;
        }
    }
}
