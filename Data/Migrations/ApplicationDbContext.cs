using Microsoft.EntityFrameworkCore;

namespace GTMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GTMS.Models.Player> Player { get; set; }
    }
}
