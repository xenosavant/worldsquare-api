using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stellmart.Context.Entities;

namespace Stellmart.Context
{
    public class StellmartContext : IdentityDbContext<IdentityUser>
    {
        public StellmartContext(DbContextOptions<StellmartContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<User> SecurityQuestion { get; set; }

        public DbSet<User> KeyRecoveryStep { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(
                user => user.Email).IsUnique(true);

            modelBuilder.Entity<User>().HasIndex(
                user => user.Username).IsUnique(true);

            modelBuilder.Entity<User>()
                .HasMany(u => u.KeyRecoverySteps);

            modelBuilder.Entity<KeyRecoveryStep>()
                .HasOne(krs => krs.SecurityQuestion);
        }
    }

    public interface IStellmartEntity
    {
        int Id { get; set; }
    }
}
