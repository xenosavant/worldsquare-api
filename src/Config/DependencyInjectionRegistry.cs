using AutoMapper;
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
using Stellmart.Services;
using StructureMap;
using System;
using System.Net.Http;
using MaxMind.GeoIP2;
using Yoti.Auth;

namespace Stellmart.Api.Config
{
    public class DependencyInjectionRegistry : Registry
    {
        public DependencyInjectionRegistry(IConfiguration configuration)
        {
            Scan(x =>
            {
                x.AssemblyContainingType<Startup>();
                x.LookForRegistries();
                x.AddAllTypesOf<Profile>();
                x.AddAllTypesOf<IKycService>();
                x.WithDefaultConventions();
            });

            For<IUserStore<ApplicationUser>>().Use<ApplicationUserStore>();
            For<UserManager<ApplicationUser>>().Use<UserManager<ApplicationUser>>();
            For<IRepository>().Use<Repository<ApplicationDbContext>>();
            For<IShippingService>().Singleton().Use<EasyPostService>();
            For<IHttpContextAccessor>().Singleton().Use<HttpContextAccessor>();
            For<IMapper>().Use(() => Mapper.Instance);
            For<IHorizonService>().Singleton().Use<HorizonService>();


            For<Server>()
                .Singleton()
                .Use(new Server(configuration["HorizonSettings:Server"] ,
                    new HttpClient
                    {
			BaseAddress = new Uri(configuration["HorizonSettings:Server"])
                    }
                ));

            For<ISearchService>()
                .Singleton()
                .Use(new AzureSearchService(
                    new SearchServiceClient(configuration["AzureSearch:ServiceName"], new SearchCredentials(configuration["AzureSearch:ApiKey"]))
                    ));

            For<YotiClient>()
                .Singleton()
                .Use(new YotiClient(configuration["YotiSettings:SdkId"], PemHelper.LoadPemFromString(configuration["YotiSettings:Pem"])));
            var path = GeoIpHelper.GetGeoIpDatabaseFilename();
            For<IGeoIP2DatabaseReader>().Singleton().Use(new DatabaseReader(path));
        }
    }
}
