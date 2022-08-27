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
        public bool IsClient(string email, out ClientTab? client)
        {
            client = null;
            
            if (string.IsNullOrEmpty(email)) return false;

            if (_dbContext?.ClientTab?.Where(x => x.Email.ToLower() == email.ToLower()).Count() == 0)
            {
                return false;
            }

            client = _dbContext?.ClientTab?.Where(x => x.Email.ToLower() == email.ToLower()).Include(c => c.users).First();
            
            return client != null;
        }

        public void SetNewClient(string userName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            int newClnum = _dbContext.ClientTab.Count() == 0 ? 1 : _dbContext.ClientTab.Select(c => c.Id).Distinct().ToList().Max() + 1;

            ClientTab clientTab = new()
            {
                Id = newClnum,
                ClientName = userName,
                Email = email,
                Pwd = password
            };
            _dbContext.ClientTab.Add(clientTab);
            _dbContext.SaveChanges();

            var param = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@clnum",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.InputOutput,
                Size = sizeof(int),
                Value = newClnum
            };
            _dbContext.Database.ExecuteSqlRaw("InitNewClNum @clnum", param);

        }
 
    }
}
