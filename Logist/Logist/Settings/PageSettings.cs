using Microsoft.EntityFrameworkCore;
using Logist.Data;
using Logist.Data.Pages;

namespace Logist.Settings
{
    public class PageSettings
    {
        private readonly AppFactory? _dbContext;
        public string ErrorMessage { get; set; }

        public PageSettings(IDbContextFactory<AppFactory> dbContext)
        {
            _dbContext = dbContext.CreateDbContext();
        }

        public List<LCustSetting>? GetSetting(int clnum,  string viewSetting)
        {
            ///
            // По умолчанию все настройки лежат с параметром clnum = 0
            // Проверяем, что для указанного клиента нет индивидиальных настроек!
            ///
            if (clnum != 0)
            {
                try
                {
                    var existView = _dbContext.lCustSettings
                        .Count(s => s.clnum == clnum && s.Viewname.ToLower() == viewSetting.ToLower());
                    
                    if (existView == 0)
                    {
                        clnum = 0;
                    }
                }
                catch
                {
                    ErrorMessage = "Не удалось прочитать настроечную таблицу.";
                    return null;
                }
                
            }

            try
            {
                return _dbContext.lCustSettings.Where(s => s.clnum == clnum &&
                                                                 s.Viewname.ToLower() == viewSetting.ToLower()).ToList();
            }
            catch
            {
                ErrorMessage = "Не удалось прочитать настроечную таблицу.";
                return null;
            }
        }

    }
}
