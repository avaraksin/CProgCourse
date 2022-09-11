using Microsoft.EntityFrameworkCore;
using Logist.Settings;
using Logist.Data.Pages;

namespace Logist.Data.MainData
{
    public class CtrlLCust
    {
        private readonly AppFactory? _dbContext;
        private readonly PageSettings _pageSettings;

       

        public CtrlLCust(IDbContextFactory<AppFactory> dbContext, PageSettings pageSettings)
        {
            _dbContext = dbContext.CreateDbContext();
            _pageSettings = pageSettings;
        }

        public async Task<List<LCust>?> GetlCust(int clnum)
        {
            try
            {
                var list = await _dbContext.lcust.Where(l => l.clnum == clnum).ToListAsync();
                return list;
            }
            catch {}

            return null;
        }

        public object GetListField(LCust lCust, string fieldname)
        {
            if (lCust == null || string.IsNullOrEmpty(fieldname))
            {
                return null;
            }

            switch (fieldname.ToLower())
            {
                case "lot":
                    return lCust.lot;
                case "product":
                    return lCust.product;
                default:
                    return null;
            }
        }
    }
}
