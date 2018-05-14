using Microsoft.AspNetCore.Identity;
using Stellmart.Context;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Look for any users.
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            var user = new ApplicationUser
            {
                Email = "admin@test.com",
                UserName = "admin@test.com"
            };

            var password = "Admin1234!";
            await userManager.CreateAsync(user, password);
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
        }
    }
}
