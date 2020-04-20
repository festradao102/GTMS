using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GTMS.Models;

namespace GTMS.Data
{
    public class GtmsContext : DbContext
    {
        public GtmsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Team>().ToTable("Team");
            builder.Entity<Player>().ToTable("Player");
        }

        public DbSet<GTMS.Models.Team> Teams { get; set; }
        public DbSet<GTMS.Models.Player> Players { get; set; }
    }
}
