using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adelanka.DAL;
using Adelanka.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Adelanka
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
            services.AddControllers();

            //Enable CORES
            services.AddCors(c => { c.AddPolicy("AllowOrigin", o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });

            services.AddControllersWithViews().AddNewtonsoftJson(o =>
            o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<AdelankaDBContext>(o => o.UseSqlServer(Configuration.GetConnectionString("AdelankaDBConnection")));

            services.AddTransient<IGenericRepository<SystemUser>, GenericRepository<SystemUser>>();
            services.AddTransient<IGenericRepository<UserNote>, GenericRepository<UserNote>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}