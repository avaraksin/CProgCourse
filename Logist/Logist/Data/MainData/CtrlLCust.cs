using Microsoft.EntityFrameworkCore;
using Logist.Settings;
using Logist.Data.Pages;
using Logist.Interfaces;

namespace Logist.Data.MainData
{
    public class CtrlLCust
    {
        private readonly AppFactory? _dbContext;
        private readonly ICashDictionary _cash;
       
        private List<LCustSetting> fieldSetting ;
        private List<Listname> listnames;

        public CtrlLCust(IDbContextFactory<AppFactory> dbContext, ICashDictionary cashDictionary)
        {
            _dbContext = dbContext.CreateDbContext();
            _cash = cashDictionary;
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

        public void SetCommonParams()
        {
            fieldSetting = _cash.settings;
            listnames = _cash.listDict;
        }


        public object GetListField(LCust lCust, string fieldname)
        {
            if (lCust == null || string.IsNullOrEmpty(fieldname))
            {
                return null;
            }

            object? result = null;

            switch (fieldname.ToLower())
            {
                case "lot":
                    result = lCust.lot;
                    break;
                case "product":
                    result = lCust.product;
                    break;
                case "supplier":
                    result = lCust.supplier;
                    break;
                case "etd_plan":
                    result = lCust.ETD_plan;
                    break;
                default:
                    break;
            }

            SetCommonParams();

            if (result != null)
            {
                switch (fieldSetting.Where(x => x.fieldname.ToLower() == fieldname.ToLower()).Select(x => x.FType).FirstOrDefault().ToLower())
                {
                    case "l":
                        var tabname = fieldSetting.Where(x => x.fieldname.ToLower() == fieldname.ToLower()).Select(x => x.TabName).FirstOrDefault().ToLower();
                        if (tabname == "listname")
                        {
                            var key = fieldSetting.Where(x => x.fieldname.ToLower() == fieldname.ToLower()).Select(x => x.Key1).FirstOrDefault();
                            result = listnames.Where(x => x.Idlist == key && x.id == (int)result).Select(x => x.Name).FirstOrDefault();
                        }
                        break;

                    case "d":
                        result = ((DateTime)result).ToString("dd.MM.yyyy");
                        break;
                    default:
                        result = result?.ToString();
                        break;
                }
            }

            return result;
        }
    }
}
