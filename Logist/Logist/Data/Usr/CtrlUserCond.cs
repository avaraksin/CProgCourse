using Microsoft.EntityFrameworkCore;
using Logist.Data;
using Logist.Settings;
using Logist.Data.Pages;

namespace Logist.Data.Usr
{
    public class CtrlUserCond
    {
        private readonly AppFactory _dbContext;
        private readonly PageSettings _pageSettings;
        public string ErrMessage { get; set; }
        public CtrlUserCond(IDbContextFactory<AppFactory> dbContext, PageSettings pageSettings)
        {
            _dbContext = dbContext.CreateDbContext();
            _pageSettings = pageSettings;
        }

        public  List<UserCond>? GetUserCond(int clnum)
        {
            try
            {
                return  _dbContext.userCond.Where(x => x.clnum == clnum).ToList();
            }
            catch(Exception ex)
            {
                ErrMessage = $"Ошибка при получении настроек пользователя ({ex.Message})";
            }
            return null;
        }

        public List<string?>? GetVisibleFields(int clnum, int usernum, string tabname, out List<string?>? otherFields)
        {
            otherFields = new List<string>();
            var userCond = GetUserCond(clnum);
            if (userCond == null) return new List<string>();

            int idCond = 0;
            if (userCond.Count(x => x.idUser == usernum
                                     && x.name.ToLower() == tabname.ToLower()
                                     && x.id == 0
                                     && x.id2 == 0) != 0)
            {

                idCond = userCond.First(x => x.idUser == usernum
                                        && x.name.ToLower() == tabname.ToLower()
                                        && x.id == 0
                                        && x.id2 == 0).idCond;
            }

            List<LCustSetting>? pageSettings = _pageSettings.GetSetting(clnum, tabname);

            if (idCond == 0)
            {
                return pageSettings?.Where(x => x.FVisible == 1).Select(x => x.fieldname).ToList();
            }

            List<string?> selectedFields = userCond.Where(x => x.idUser == usernum && x.idCond == idCond && x.id > 0 && x.id2 == 0).Select(x => x.name).ToList();

            otherFields = pageSettings?.Where(x => !selectedFields.Contains(x.fieldname)).Select(x => x.fieldname).ToList();

            return selectedFields;
        }
    }
}
