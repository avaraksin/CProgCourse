using Logist.Data.Pages;
using Logist.Data.Usr;
using Logist.Interfaces;
using Logist.Settings;
using Microsoft.EntityFrameworkCore;

namespace Logist.Data
{
    public class CashDictionary : ICashDictionary
    {
        private readonly AppFactory _appFactory;
        private readonly PageSettings _pageSettings;

        public List<Listname> listDict { get; set; }
        public List<Users> users { get; set; }

        public List<LCustSetting> settings { get; set; }

        public CashDictionary(PageSettings settings, IDbContextFactory<AppFactory> dbContext)
        {
            _pageSettings = settings;
            _appFactory = dbContext.CreateDbContext();
        }

        public Task GetListname(int clnum)
        {
            listDict = _appFactory.listname.Where(x => x.IsDel == 0 && x.clnum == clnum).ToList();
            return Task.CompletedTask;
        }
        public Task GetUsers(int clnum)
        {
            users = _appFactory.users.Where(x => x.isdel == 0 && x.clnum == clnum).ToList();
            return Task.CompletedTask;
        }

        public Task GetSettings(int clnum)
        {
            settings = _pageSettings.GetSetting(clnum, "lCust");
            return Task.CompletedTask;
        }

        public void Refresh(int clnum)
        {
            GetListname(clnum).Wait();
            GetUsers(clnum).Wait();
            GetSettings(clnum).Wait();
        }


    }
}
