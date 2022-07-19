using GifSrvice.Data;


namespace GifSrvice.Interface
{
    public interface IGif
    {
        public Gifdata GetImage(string param);
        public string GetGifUrl(Gifdata gifdata);
    }
}
