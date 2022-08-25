using Microsoft.EntityFrameworkCore;

namespace Logist.Data
{
    public class ProgramLogin
    {
        private AppFactory _dbContext;
        public ProgramLogin(IDbContextFactory<AppFactory> dbContext)
        {
            _dbContext = dbContext.CreateDbContext();
        }
        public bool IsClient(string email, string pwd, out ClientTab? client)
        {
            client = null;
            
            if (string.IsNullOrEmpty(email)) return false;

            if (string.IsNullOrEmpty(pwd))
            {
                client = _dbContext?.ClientTab?.Where(x => x.Email.ToLower() == email.ToLower() &&
                    (x.Pwd == null || x.Pwd == "")).First();
            }
            else
            {
                client = _dbContext?.ClientTab?.Where(x => x.Email.ToLower() == email.ToLower() &&
                    x.Pwd == pwd).First();
            }

            return client != null;
        }
    }
}
