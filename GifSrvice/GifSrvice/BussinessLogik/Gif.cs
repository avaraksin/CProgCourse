using GifSrvice.Data;
using Newtonsoft.Json;

namespace GifSrvice.BussinessLogik
{
    public class Gif
    {
        private static string app_id = "GTRyejAYZqD0cfjcbjh74d8V6tfY0YEK";
        private static string url = "https://api.giphy.com/v1/gifs/random?";

        private static Gif? instance;

        static HttpClient client = new HttpClient();

        public static Gif GetInstance()
        {
            if (instance == null)
            {
                instance = new Gif();
            }
            return instance;
        }

        public Gifdata GetImage(string param)
        {
            string fulpath = url + "api_key=" + app_id;

            fulpath += ";tag=" + param;
            fulpath += ";rating=g";

            fulpath = $"https://api.giphy.com/v1/gifs/random?api_key=GTRyejAYZqD0cfjcbjh74d8V6tfY0YEK&tag=" + param + $"&rating=g";

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
