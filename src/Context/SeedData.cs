using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stellmart.Context;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public class SeedData
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            // ensure database exists and migration applied
            context.Database.Migrate();

            // Look for any users.
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            var user = new ApplicationUser
            {
                Email = configuration["SeedData:InitialAdminUser"],
                UserName = configuration["SeedData:InitialAdminUser"]
            };

            var password = configuration["SeedData:InitialAdminPassword"];
            userManager.CreateAsync(user, password);
            userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
        }
    }
}
