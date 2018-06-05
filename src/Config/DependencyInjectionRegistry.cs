using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stellmart.Api.Context;
using Stellmart.Api.DataAccess;
using Stellmart.Context;
using StructureMap;

namespace Stellmart.Api.Config
{
    public class DependencyInjectionRegistry : Registry
    {
        public DependencyInjectionRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<Startup>();
                x.Assembly("Stellmart.Api");
                x.LookForRegistries();
                x.AddAllTypesOf<Profile>();
                x.WithDefaultConventions();
            });

            For<IUserStore<ApplicationUser>>().Use<ApplicationUserStore>();
            For<UserManager<ApplicationUser>>().Use<UserManager<ApplicationUser>>();
            For<IRepository>().Use<Repository<ApplicationDbContext>>();

            For<IHttpContextAccessor>().Singleton().Use<HttpContextAccessor>();
            For<IMapper>().Use(() => Mapper.Instance);
        }
    }
}
