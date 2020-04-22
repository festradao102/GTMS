using GTMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GTMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.Entity<Team>().ToTable("Team");
            builder.Entity<Player>().ToTable("Player");
            builder.Entity<ConfigValues>().ToTable("ConfigValues");
        }

        //definir entidades del modelo a mapear
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<ConfigValues> ConfigValues {get; set;}
    }
}
