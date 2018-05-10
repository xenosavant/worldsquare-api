using Microsoft.EntityFrameworkCore;
using Stellmart.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Context
{
    public class StellmartContext : DbContext
    {
        public StellmartContext(DbContextOptions<StellmartContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<User> SecurityQuestion { get; set; }

        public DbSet<User> KeyRecoveryStep { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
