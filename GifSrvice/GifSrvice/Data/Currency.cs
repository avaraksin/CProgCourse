namespace GifSrvice.Data
{
    public class Currency
    {
        public string app_id = "eac80a2c15f84765baa70420b28c6a88";
        public string? Base;
        public string? symbols;

        public Currency()
        {
             new Currency("USD", "RUB");
        }

        public Currency(string? Base, string? symbols)
        {
            this.Base = Base;
            this.symbols = symbols;
        }
    }

    public class Cur_Report
    {
        public string? disclaimer;
        public string? license;
        public string? timestamp;
        public string? Base;
        public string? rates;
    }
}
