using Microsoft.EntityFrameworkCore;
using TestDirectumRX.Models;

namespace TestDirectumRX.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Departament> Departaments { get; set; }

        public DbSet<Empoyee> Empoyees { get; set; }
    }
}
