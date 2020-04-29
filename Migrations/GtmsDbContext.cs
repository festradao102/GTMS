using GTMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GTMS.Data
{
    public class GtmsDbContext : DbContext
    {
        public GtmsDbContext(DbContextOptions<GtmsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.Entity<Team>().ToTable("Team");
            builder.Entity<Player>().ToTable("Player");
            builder.Entity<ConfigValues>().ToTable("ConfigValues");
            builder.Entity<Referee>().ToTable("Referee");
            builder.Entity<Message>().ToTable("Message");
        }

        //definir entidades del modelo a mapear
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<ConfigValues> ConfigValues {get; set;}
        public DbSet<Referee> Referees {get; set;} 
        public DbSet<Message> Messages {get; set;}
    }
}
