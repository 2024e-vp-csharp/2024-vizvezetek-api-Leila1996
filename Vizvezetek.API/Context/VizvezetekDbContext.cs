using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Vizvezetek.API.Models;

namespace Vizvezetek.API.Context
{
    public class VizvezetekDbContext : DbContext
    {
        public VizvezetekDbContext(DbContextOptions<VizvezetekDbContext> options) : base(options) { }
        public DbSet<Munkalap> Munkalapok { get; set; }
        public DbSet<Hely> hely { get; set; }
        public DbSet<Szerelo> szerelo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Munkalap>()
                .ToTable("munkalap"); // Ha az adatbázisban ez a táblanév

            base.OnModelCreating(modelBuilder);
        }
    }
}
