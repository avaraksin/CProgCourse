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

        public async Task SetNewClient(string userName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            int newClnum = _dbContext.ClientTab.Count() + 1;

            ClientTab clientTab = new()
            {
                Id = newClnum,
                ClientName = userName,
                Email = email,
                Pwd = password
            };
            
            await _dbContext.ClientTab.AddAsync(clientTab);
            await _dbContext.SaveChangesAsync();

            var param = new Microsoft.Data.SqlClient.SqlParameter
            {
                ParameterName = "@clnum",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.InputOutput,
                Size = sizeof(int),
                Value = newClnum
            };
            await _dbContext.Database.ExecuteSqlRawAsync("InitNewClNum @clnum", param);

        }
 
    }
}
