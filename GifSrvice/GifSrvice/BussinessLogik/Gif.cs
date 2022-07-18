using GifSrvice.Data;
using Newtonsoft.Json;

namespace GifSrvice.BussinessLogik
{
    public class Gif
    {
        private IHttpClientFactory _httpClientFactory;

        private static Gif? instance;

        public Gif(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        

        public Gifdata GetImage(string param)
        {
            string fulpath = url + "api_key=" + app_id;

            fulpath += ";tag=" + param;
            fulpath += ";rating=g";

            fulpath = $"https://api.giphy.com/v1/gifs/random?api_key=GTRyejAYZqD0cfjcbjh74d8V6tfY0YEK&tag=" + param + $"&rating=g";

            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();

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
