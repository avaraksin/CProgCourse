using Logist.Data;
using Logist.Common;
using Logist.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Logist.Data.LogFolder
{
    public enum CommonLogStatus
    {
        Start = 1,
        TrySignUp = 2,
        SignIn = 3,
        NavigateTo = 4
    }
    public class CommonLoger
    {
        private readonly AppFactory _context;

        public CommonLoger(IDbContextFactory<AppFactory> dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = dbContext.CreateDbContext();
        }

        public async Task GetUser(string IPaddr)
        {
            var lLog = SetCommonParam();
            lLog.oper = (int) CommonLogStatus.Start;
            lLog.CompName = IPaddr;

            UserSettings.ClientIP = IPaddr;

            await SaveLogRow(lLog);
        }

        public async Task SetRegistration(string IPaddr, string email, string pass, bool status = true)
        {
            var lLog = SetCommonParam();
            
            lLog.oper = (int) CommonLogStatus.TrySignUp;
            lLog.WinUser = email;
            lLog.param3 = pass;
            lLog.Stats = status ? 1 : 0;
            lLog.CompName = IPaddr;

            await SaveLogRow(lLog);
        }

        public async Task SetSignIn(string IPaddr, int clnum, int? userid, string email)
        {
            
            var lLog = SetCommonParam(clnum);
            
            lLog.oper = (int) CommonLogStatus.SignIn;
            lLog.ProgUser = userid;
            lLog.WinUser = email;
            lLog.Stats = 1;
            lLog.CompName = IPaddr;

            await SaveLogRow(lLog);
        }

        public async Task SetNavigate(string IPaddr, int clnum, int? user, FormOnDisplay page)
        {
            if (clnum == 0) clnum = -1;
            // todo всю инициализацию перенести в констуктор
            var lLog = SetCommonParam(clnum);

            lLog.Param1 = (int)page;
            lLog.ProgUser = user;
            lLog.oper = (int)CommonLogStatus.NavigateTo;
            lLog.CompName = IPaddr;

            await SaveLogRow(lLog);
        }

        private LLog SetCommonParam(int clnum = -1)
        {
            DateTime thisDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            return new LLog()
            {
                LogDt = thisDate,
                LogTime = DateTime.Now.ToString("HH.mm.ss"),
                clnum = clnum,
                id = _context.lLog.Count(l => l.clnum == clnum && l.LogDt == thisDate) + 1
            };
        }
        
        private async Task SaveLogRow(LLog lLog)
        {
            try
            {
                await _context.lLog.AddAsync(lLog);
                await _context.SaveChangesAsync();

            }
            catch { }
        }

    }
}
