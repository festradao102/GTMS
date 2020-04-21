using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GTMS.Models;

namespace GTMS.Data
{
    public class GtmsContext : DbContext
    {
      //  The DbContextOptions instance carries configuration information such as: The database provider to use, 
      // Any necessary connection string or identifier of the database instance, etc...
        public GtmsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Team>().ToTable("Team");
            builder.Entity<Player>().ToTable("Player");
        }

        //definir entidades del modelo a mapear
        public DbSet<GTMS.Models.Team> Teams { get; set; }
        public DbSet<GTMS.Models.Player> Players { get; set; }
    }
}
