using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

            For<IHttpContextAccessor>().Singleton().Use<HttpContextAccessor>();
            For<IMapper>().Use(() => Mapper.Instance);
            For<IHorizonService>().Singleton().Use<HorizonService>();
            For<IContractService>().Singleton().Use<ContractService>();

            For<Server>()
                .Singleton()
                .Use(new Server(configuration["HorizonSettings:Server"])
                {
                    HttpClient = new HttpClient
                    {
			BaseAddress = new Uri(configuration["HorizonSettings:Server"])
                    }
                });

            For<YotiClient>()
                .Singleton()
                .Use(new YotiClient(configuration["YotiSettings:SdkId"], PemHelper.LoadPemFromString(configuration["YotiSettings:Pem"])));
        }
    }
}
