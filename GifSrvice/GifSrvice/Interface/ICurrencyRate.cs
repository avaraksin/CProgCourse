using GifSrvice.BussinessLogik;
using GifSrvice.Data;

namespace GifSrvice.Interface
{
    public interface ICurrencyRates
    {
        Task<CurReport> GetRate(DateTime date);
        public int IsCurrencyRiseFromYesterday();
    }
}
