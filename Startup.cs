using EntityFramework.Intro.Database;
using EntityFramework.Intro.Helpers;
using EntityFramework.Intro.Middleware;
using EntityFramework.Intro.Models.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Intro
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
    public Startup(IConfiguration config)
    {
        Configuration = config;
    }

    
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("test")=="test")
            {
                services.AddDbContext<DataContext>(opts =>
                {
                    //opts.UseSqlServer(Configuration["ConnectionStrings:ProductConnection"]);
                    opts.UseSqlite(@"Data Source=products.db");
                    opts.EnableSensitiveDataLogging(true);
                });
            }
            else
                services.AddDbContext<DataContext>(
                    opts =>
                    {
                        opts.UseSqlServer(Configuration["ConnectionStrings:ProductConnection"]);
                        //opts.UseSqlite(@"Data Source=products.db");
                        opts.EnableSensitiveDataLogging(true);
                    });
            
            
           // services.AddControllers();
            services.AddSingleton(new DatabaseConfig { Name = Configuration["DatabaseName"]  });
            services.AddSingleton<IDatabaseProduct, DatabaseProduct>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMiddleware<Test>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                // endpoints.MapWebService();
                //endpoints.MapControllers();
            });
            SeedData.SeedDatabase(context);
            serviceProvider.GetService<IDatabaseProduct>().Setup();
        }
    }
}
