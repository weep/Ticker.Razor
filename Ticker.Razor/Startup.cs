using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticker.Razor.Infrastructure;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Ticker.Razor
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
            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Audience = Configuration["Settings:Authentication:ApiName"];
                o.Authority = Configuration["Settings:Authentication:Authority"];
                //o.RequireHttpsMetadata = !CurrentEnvironment.IsDevelopment();
            });

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                //options.Conventions.AuthorizeFolder("/portfolio");
                options.Conventions.AuthorizeFolder("/account/login");
                //options.Conventions.AllowAnonymousToPage("/account/login");
            });

            services.AddSingleton<HttpClient>();
            services.AddTransient<IMarketApi, CoinMarketCapApi>();
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
                app.UseExceptionHandler("/Error");
            }

            app.Use(async (context, next) =>
            {
                await next.Invoke();
            });

            app.UseStaticFiles();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
