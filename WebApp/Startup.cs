using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Sentry;
using SpacePlanetsDAL.Services;
using Swashbuckle.AspNetCore.Swagger;
using WebApp.Filters;

namespace WebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            var client = new MongoClient("mongodb://localhost:27017/SpacePlanetsDev");

            services.AddScoped<IMongoClient>(c => client);
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddCookieManager(options =>
                {
                    options.AllowEncryption = false;
                    options.ThrowForPartialCookies = true;
                    options.ChunkSize = null;
                    options.DefaultExpireTimeInDays = 7;
                }
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("apikey", new ApiKeyScheme
                {
                    Description = "Authorization header using a token scheme. Not required if supplying api username and password. Example: \"{token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });


                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                c.IncludeXmlComments(@"SpLib.xml");
                c.OperationFilter<SwaggerAuthorizationHeaderFilter>();
            });
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
