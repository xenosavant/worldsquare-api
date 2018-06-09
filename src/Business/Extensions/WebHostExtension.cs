using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stellmart.Api.Context;
using Stellmart.Context;
using System;

namespace Stellmart.Api.Business.Extensions
{
    public static class WebHostExtension
    {
        public static IWebHost Migrate(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                    SeedData.Initialize(context, userManager, configuration);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            return webhost;
        }
    }
}
