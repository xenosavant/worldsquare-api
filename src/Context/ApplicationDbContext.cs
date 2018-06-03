using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities;
using System.Linq;

namespace Stellmart.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Area> Areas { get; set; }

        public DbSet<GeoLocation> GeoLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Area>()
               .HasOne(a => a.GeoLocation);
        }
    }
}
