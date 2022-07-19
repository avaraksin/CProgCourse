using GifSrvice.BussinessLogik;
using GifSrvice.Data;

namespace GifSrvice.Interface
{
    public interface ICurrencyRates
    {
        CurReport GetRate(DateTime date);
        public int DynRates();
    }
}
