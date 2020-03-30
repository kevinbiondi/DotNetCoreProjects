using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;

namespace OdeToFood
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
            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            //Biondi--added only for the canned data for data-remove when adding a database
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            //Biondi --added to switch over to use SqlRestaurantData
            //services.AddScoped<IRestaurantData, SqlRestaurantData>();

            //to run in IIS first with the inmemory data then later add the sql server database
            services.AddScoped<IRestaurantData, InMemoryRestaurantData>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(SayHelloMiddleware);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseNodeModules(env); This was the instructors middle ware to work with node js (datatables) outside of wwwroot
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        private RequestDelegate SayHelloMiddleware(RequestDelegate next)
        {
            return async ctx =>
            {
                if(ctx.Request.Path.StartsWithSegments("/hello")) 
                { 
                    await ctx.Response.WriteAsync("Hello, Wordld!");
                } 
                else
                {
                    await next(ctx);
                }
            };
        }
    }
}
