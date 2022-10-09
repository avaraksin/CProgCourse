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

        public async Task<List<LCust>> GetVisibleCust(int clnum)
        {
            try
            {
                var list = await _dbContext.lcust.Where(l => l.clnum == clnum ).ToListAsync();
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
                case "id":
                    result = lCust.id;
                    break;
                case "yr":
                    result = lCust.yr;
                    break;
                case "isdel":
                    result = lCust.isdel;
                    break;
                case "client":
                    result = lCust.Client;
                    break;
                case "splace":
                    result = lCust.splace;
                    break;
                case "warehouse":
                    result = lCust.warehouse;
                    break;
                case "luser":
                    result = lCust.luser;
                    break;
                case "delivery":
                    result = lCust.delivery;
                    break;
                case "weightp":
                    result = lCust.weightp;
                    break;
                case "cntp":
                    result = lCust.cntp;
                    break;
                case "weight":
                    result = lCust.weight;
                    break;
                case "cnt":
                    result = lCust.cnt;
                    break;
                case "weight_w":
                    result = lCust.weight_w;
                    break;
                case "cnt_w":
                    result = lCust.cnt_w;
                    break;
                case "valplan":
                    result = lCust.valplan;
                    break;
                case "valfact":
                    result = lCust.valfact;
                    break;
                case "etd_fact":
                    result = lCust.ETD_FACT;
                    break;
                case "eta_plan":
                    result = lCust.ETA_plan;
                    break;
                case "eta_fact":
                    result = lCust.ETA_fact;
                    break;
                case "transi":
                    result = lCust.transi;
                    break;
                case "vallogist":
                    result = lCust.vallogist;
                    break;

                case "pay1_sum":
                    result = lCust.pay1_sum;
                    break;
                case "pay1_bill":
                    result = lCust.pay1_bill;
                    break;
                case "pay1_dplan":
                    result = lCust.pay1_dplan;
                    break;
                case "pay1_dfact":
                    result = lCust.pay1_dfact;
                    break;

                case "pay2_sum":
                    result = lCust.pay2_sum;
                    break;
                case "pay2_bill":
                    result = lCust.pay2_bill;
                    break;
                case "pay2_dplan":
                    result = lCust.pay2_dplan;
                    break;
                case "pay2_dfact":
                    result = lCust.pay2_dfact;
                    break;

                case "pay3_sum":
                    result = lCust.pay3_sum;
                    break;
                case "pay3_bill":
                    result = lCust.pay3_bill;
                    break;
                case "pay3_dplan":
                    result = lCust.pay3_dplan;
                    break;
                case "pay3_dfact":
                    result = lCust.pay3_dfact;
                    break;

                case "docs":
                    result = lCust.docs;
                    break;
                case "doc_dplan":
                    result = lCust.doc_dplan;
                    break;
                case "doc_dfact":
                    result = lCust.doc_dfact;
                    break;
                case "cmnt":
                    result = lCust.cmnt;
                    break;
                case "lcmnt":
                    result = lCust.lcmnt;
                    break;
                case "chdate":
                    result = lCust.chdate;
                    break;
                case "cuser":
                    result = lCust.cuser;
                    break;
                case "clnum":
                    result = lCust.clnum;
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
                    
                    case "i":
                        result = ((int)result).ToString("0");
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
