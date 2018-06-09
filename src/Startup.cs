using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stellmart.Api.Config;
using Stellmart.Api.Context;
using Stellmart.Api.Data.Settings;
using Stellmart.Context;
using StructureMap;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Stellmart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(Configuration.GetSection("HostSettings:AppUrl").Value.ToString())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvcCore();
            services.AddMvc();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(o =>
                {
                    o.Authority = Configuration.GetSection("IdentityServerSettings:AuthUrl").Value;
                    o.ApiName = "api1";
                    o.ApiSecret = Configuration.GetSection("IdentityServerSettings:ClientSecret").Value;
                    o.EnableCaching = true;
                    o.RequireHttpsMetadata = false;
                    o.SupportedTokens = SupportedTokens.Both;
                });

            services.Configure<HorizonSettings>(Configuration.GetSection("HorizonSettings"));

            // Add di framework
            var container = new Container(new DependencyInjectionRegistry(Configuration));
            container.Populate(services);

            services.AddAutoMapper(cfg => cfg.ConstructServicesUsing(container.GetInstance));
            Mapper.AssertConfigurationIsValid();

            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (errorFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                        }

                        await context.Response.WriteAsync("There was an error");
                    });
                });
            }

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Default}/{action=Index}/{id?}");
            });
        }
    }
}
