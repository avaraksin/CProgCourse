using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
namespace Logist.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)  {}

        public DbSet<lists> lists { get; set; }
        public DbSet<Listname> listname { get; set; }
    }
}
