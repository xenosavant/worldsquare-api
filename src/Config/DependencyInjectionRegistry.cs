using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stellar_dotnetcore_sdk;
using Stellmart.Api.Business.Constants;
using Stellmart.Api.Context;
using Stellmart.Api.DataAccess;
using Stellmart.Context;
using Stellmart.Services;
using StructureMap;
using System;
using System.Net.Http;

namespace Stellmart.Api.Config
{
    public class DependencyInjectionRegistry : Registry
    {
        public DependencyInjectionRegistry(IConfiguration configuration)
        {
            Scan(x =>
            {
                x.AssemblyContainingType<Startup>();
                x.Assembly("Stellmart.Api");
                x.LookForRegistries();
                x.AddAllTypesOf<Profile>();
                x.WithDefaultConventions();
            });

            For<IUserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
            For<UserManager<ApplicationUser>>().Use<UserManager<ApplicationUser>>();
            For<IRepository>().Use<Repository<ApplicationDbContext>>();

            For<IHttpContextAccessor>().Singleton().Use<HttpContextAccessor>();
            For<IMapper>().Use(() => Mapper.Instance);
            For<IHorizonService>().Singleton().Use<HorizonService>();
            For<IContractService>().Singleton().Use<ContractService>();

            var horizonServer = configuration["HorizonSettings:Server"];

            For<Server>()
                .Singleton()
                .Use(new Server(horizonServer)
                {
                    HttpClient = new HttpClient
                    {
                        BaseAddress = new Uri(horizonServer)
                    }
                });

            For<Network>()
                .Singleton()
                .Use(horizonServer.Contains("testnet") ? new Network(Horizon.NetworkPassphraseTestnet) : new Network(Horizon.NetworkPassphrasePublic));
        }
    }
}
