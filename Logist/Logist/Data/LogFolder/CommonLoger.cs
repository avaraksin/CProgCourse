using Logist.Data;
using Logist.Common;
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
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly string? currentIP;

        public CommonLoger(IDbContextFactory<AppFactory> dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = dbContext.CreateDbContext();
            currentIP = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        }

        public async Task GetUser()
        {
            var lLog = SetCommonParam();
            lLog.oper = (int) CommonLogStatus.Start;
            await SaveLogRow(lLog);
        }

        public async Task SetRegistration(string email, string pass, bool status = true)
        {
            var lLog = SetCommonParam();
            
            lLog.oper = (int) CommonLogStatus.TrySignUp;
            lLog.WinUser = email;
            lLog.param3 = pass;
            lLog.Stats = status ? 1 : 0;

            await SaveLogRow(lLog);
        }

        public async Task SetSignIn(int clnum, int? userid, string email)
        {
            
            var lLog = SetCommonParam(clnum);
            
            lLog.oper = (int) CommonLogStatus.SignIn;
            lLog.ProgUser = userid;
            lLog.WinUser = email;
            lLog.Stats = 1;

            await SaveLogRow(lLog);
        }

        public async Task SetNavigate(int clnum, int? user, FormOnDisplay page)
        {
            if (clnum == 0) clnum = -1;
            var lLog = SetCommonParam(clnum);

            lLog.Param1 = (int)page;
            lLog.ProgUser = user;
            lLog.oper = (int)CommonLogStatus.NavigateTo;

            await SaveLogRow(lLog);
        }

        private LLog SetCommonParam(int clnum = -1)
        {
            DateTime thisDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            return new LLog()
            {
                LogDt = thisDate,
                LogTime = DateTime.Now.ToString("HH.mm.ss"),
                CompName = currentIP,

                clnum = clnum,
                id = _context.lLog.Where(l => l.clnum == clnum && l.LogDt == thisDate).ToList().Count() == 0 ? 
                    1 :
                    _context.lLog.Where(l => l.clnum == clnum && l.LogDt == thisDate).Select(n => n.id).Max() + 1
            };
        }

        private async Task SaveLogRow(LLog lLog)
        {
            try 
            {
                await Task.Run(async () =>
                {
                    await _context.lLog.AddAsync(lLog);
                    await _context.SaveChangesAsync();
                });
            }
            catch { }
        }

    }
}
