using GifSrvice.Data;


namespace GifSrvice.Interface
{
    public interface IGif
    {
        public Task<Gifdata> GetImage(string param);
        public string? GetGifUrl(Gifdata gifdata);
    }
}
