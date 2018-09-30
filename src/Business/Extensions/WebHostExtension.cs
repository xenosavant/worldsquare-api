using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities.Interfaces;
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
                    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                    var searchClient = new SearchServiceClient(configuration["AzureSearch:ServiceName"], new SearchCredentials(configuration["AzureSearch:ApiKey"]));
                    var types = context.TypesOf<ISearchable>();
                    foreach (var type in types)
                    {
                        var name = type.Name.ToLower();
                        ISearchable instance = (ISearchable)Activator.CreateInstance(type);
                        if (!searchClient.Indexes.Exists(name))
                        {
                            var index = new Index()
                            {
                                Name = name,
                                Fields = instance.GetFields()
                            };
                            searchClient.Indexes.Create(index);
                        };
                    }

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

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
