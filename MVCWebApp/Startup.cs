using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MVCWebApp.Data;
using MVCWebApp.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.Models;

namespace MVCWebApp
{
    public class Startup
    {
        //public IConfiguration Configuration { get; }
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("PersonDBConnection")));
            services.AddMvc();
            services.AddMvc().AddXmlDataContractSerializerFormatters();
            //services.AddDbContext<ApplicationDbContext>(
            //    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //formating repository pattern using sql 
           // services.AddSingleton<IPersonRepository, SQLpersonRepository>();
            //services.AddTransient<IPersonRepository, SQLpersonRepository>();
            services.AddScoped<IPersonRepository, SQLpersonRepository>(); //add repository
            services.AddHttpContextAccessor();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {

                //person route
                endpoints.MapControllerRoute(
                       name: "Personlist",
                       pattern: "{controller=Person}/{action=Index}"
                       );


                //AjaxPerson route
                endpoints.MapControllerRoute(
                    name: "Personlist",
                    pattern: "Personlist",
                    defaults: new { controller = "AjaxController", action = "Index" });
            });

        }
    }
}
