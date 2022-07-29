using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;

namespace Logist.Data
{
    public class WeatherForecastService
    {
        private IListOper _listOper;

        public WeatherForecastService(IListOper listOper)
        {
            _listOper = listOper;
        }

        
        public async Task<listname[]> GetForecastAsync()
        {
            return _listOper.GetListname().Where(u => u.Id > 1).ToArray();
        }
    }

    public class ListOper : IListOper
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ListOper(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public listname[] GetListname()
        {
            var conn = _contextFactory.CreateDbContext();
            return conn.Users.ToArray();
        }

    }
}