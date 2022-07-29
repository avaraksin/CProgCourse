using Microsoft.EntityFrameworkCore;

namespace Logist.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    
        public DbSet<listname?> Users { get; set; }
        
    }

    public class listname
    {
        // 	id	IsDel	Name		UserName	Reserved	name2	name3	id2	iParam1	id3	iParam2
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Comment { get; set; }
        //public int Idlist { get; set; }
        //public int IsDel { get; set; }
        //public string UserName { get; set; }

    }
}
