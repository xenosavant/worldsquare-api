using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stellmart.Api.Business.Policies;
using Stellmart.Api.Config;
using Stellmart.Api.Context;
using Stellmart.Api.Data.Settings;
using Stellmart.Context;
using StructureMap;
using System;

namespace Stellmart
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            Hosting = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Hosting { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
                
            services.AddIdentityCore<ApplicationUser>(options => { })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvcCore()
                .AddAuthorization(options => {
                    options.AddPolicy("TwoFactorRequired", policy =>
                        policy.Requirements.Add(new TwoFactorRequirement(true)));
                    options.AddPolicy("TwoFactorOptional", policy =>
                        policy.Requirements.Add(new TwoFactorRequirement(false)));
                })
                .AddJsonFormatters();

            services.AddMvc();

            services
            .AddAuthentication(GetAuthenticationOptions)
            .AddJwtBearer(GetJwtBearerOptions);

            services.AddSingleton<IAuthorizationHandler, TwoFactorHandler>();

            services.Configure<HorizonSettings>(Configuration.GetSection("HorizonSettings"));
            services.Configure<YotiSettings>(Configuration.GetSection("YotiSettings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<AzureSettings>(Configuration.GetSection("AzureSettings"));
            services.Configure<HostSettings>(Configuration.GetSection("HostSettings"));
            services.Configure<EasyPostSettings>(Configuration.GetSection("EasyPostSettings"));
            services.Configure<SignatureSettings>(Configuration.GetSection("SignatureSettings"));

            // Add di framework
            var container = new Container(new DependencyInjectionRegistry(Configuration));
            container.Populate(services);

            services.AddAutoMapper(cfg => cfg.ConstructServicesUsing(container.GetInstance));
            Mapper.AssertConfigurationIsValid();

            return container.GetInstance<IServiceProvider>();
        }

        private void GetAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private void GetJwtBearerOptions(JwtBearerOptions options)
        {
            options.Authority = Configuration.GetSection("IdentityServerSettings:AuthUrl").Value;
            options.Audience = "api1";
            options.RequireHttpsMetadata = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
