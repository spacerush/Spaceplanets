using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SpacePlanetsMvc.BackgroundServices;
using SpacePlanetsMvc.Hubs;
using SpacePlanetsMvc.Repositories;

namespace SpacePlanetsMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var client = new MongoClient("mongodb://localhost:27017/SpacePlanets");
            services.AddSingleton<IMongoClient>(c => client);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCookieManager(options =>
            {
                options.AllowEncryption = false;
                options.ThrowForPartialCookies = true;
                options.ChunkSize = null;
                options.DefaultExpireTimeInDays = 7;
            }
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<SpacePlanetsMvc.Services.IAuthenticationService, SpacePlanetsMvc.Services.AuthenticationService>();
            services.AddSignalR();
            IServiceProvider provider = services.BuildServiceProvider();
            services.AddHostedService<CurrentTimeWorker>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(router =>
            {
                router.MapHub<GalaxyHub>("/GalaxyHub");
            });
        }
    }
}
