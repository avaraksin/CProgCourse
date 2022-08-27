using Microsoft.EntityFrameworkCore;
using Logist.Data.Usr;

namespace Logist.Data
{
    public class AppFactory : DbContext
    {
        public AppFactory(DbContextOptions<AppFactory> options)
            : base(options) { }

        public DbSet<ClientTab> ClientTab { get; set; }
        public DbSet<Lists>? lists { get; set; }
        public DbSet<Listname>? listname { get; set; }
        public DbSet<Users> users { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lists>().HasKey(c => new { c.clnum, c.id });
            modelBuilder.Entity<Users>().HasKey(c => new { c.clnum, c.id });

            modelBuilder.Entity<Listname>(entity =>
            {
                entity.Property(e => e.Name).IsRequired(false);
                entity.Property(e => e.name2).IsRequired(false);
                entity.Property(e => e.name3).IsRequired(false);
                entity.Property(e => e.Comment).IsRequired(false);

                entity.HasKey(c => new { c.Idlist, c.id, c.id2, c.clnum });
                entity.HasOne(l => l.user).WithMany().HasForeignKey(l => new { l.clnum, l.cuser }).HasPrincipalKey(l => new { l.clnum, l.id });
            });

            modelBuilder.Entity<ClientTab>().HasMany(c => c.users).WithOne().HasForeignKey(u => new {u.clnum});
        }

    }
}
