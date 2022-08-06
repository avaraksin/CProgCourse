using Microsoft.EntityFrameworkCore;

namespace Logist.Data
{
    public class AppFactory :  DbContext
    {
        public AppFactory(DbContextOptions<AppFactory> options)
            : base(options) { }

        public DbSet<Lists>? lists { get; set; }
        public DbSet<Listname>? listnames { get; set; }
    }

    public class FactoryListname
    {
        private readonly IDbContextFactory<AppFactory> _context;

        public FactoryListname(IDbContextFactory<AppFactory> dbContext)
        {
            _context = dbContext;
        }

        public List<Listname> GetListName()
        {
            using (var context = _context.CreateDbContext())
            {
                if (context != null)
                {
                    List<Listname> list = context.listnames.ToList();
                    return list;
                }
            }
            return null;
        }
    }
}
