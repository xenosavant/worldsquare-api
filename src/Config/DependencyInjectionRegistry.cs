using System;
using System.Net.Http;
using AutoMapper;
using EasyPost;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using stellar_dotnet_sdk;
using Stellmart.Api.Business.Helpers;
using Stellmart.Api.Context;
using Stellmart.Api.DataAccess;
using Stellmart.Api.Services;
using Stellmart.Api.Services.Interfaces;
using Stellmart.Context;
using StructureMap;
using Yoti.Auth;

namespace Stellmart.Api.Config
{
    public class DependencyInjectionRegistry : Registry
    {
        public DependencyInjectionRegistry(IConfiguration configuration)
        {
            Scan(action: x =>
            {
                x.AssemblyContainingType<Startup>();
                x.LookForRegistries();
                x.AddAllTypesOf<Profile>();
                x.AddAllTypesOf<IKycService>();
                x.WithDefaultConventions();
            });

            For<IUserStore<ApplicationUser>>()
                .Use<ApplicationUserStore>();
            For<UserManager<ApplicationUser>>()
                .Use<UserManager<ApplicationUser>>();
            For<IRepository>()
                .Use<Repository<ApplicationDbContext>>();
            For<IShippingService>()
                .Singleton()
                .Use<EasyPostService>();
            For<IHttpContextAccessor>()
                .Singleton()
                .Use<HttpContextAccessor>();
            For<IMapper>()
                .Use(expression: () => Mapper.Instance);
            For<IHorizonService>()
                .Singleton()
                .Use<HorizonService>();

            For<Server>()
                .Singleton()
                .Use(new Server(configuration[key: "HorizonSettings:Server"], new HttpClient {BaseAddress = new Uri(configuration[key: "HorizonSettings:Server"])}));

            For<ISearchService>()
                .Singleton()
                .Use(new AzureSearchService(new SearchServiceClient(configuration[key: "AzureSearch:ServiceName"],
                                                                    new SearchCredentials(configuration[key: "AzureSearch:ApiKey"]))));

            For<YotiClient>()
                .Singleton()
                .Use(new YotiClient(configuration[key: "YotiSettings:SdkId"], PemHelper.LoadPemFromString(configuration[key: "YotiSettings:Pem"])));

            var path = GeoIpHelper.GetGeoIpDatabaseFilename();
            For<IGeoIP2DatabaseReader>()
                .Singleton()
                .Use(new DatabaseReader(path));

            // EasyPost
            ClientManager.SetCurrent(configuration[key: "EasyPost:ApiKey"]);
        }
    }
}