using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Stellmart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables();

                    var builtConfig = config.Build();

                    config.AddAzureKeyVault(
                        $"https://{builtConfig["KeyVault:Name"]}.vault.azure.net/",
                        builtConfig["KeyVault:ClientId"],
                        builtConfig["KeyVault:ClientSecret"]);
                })
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
