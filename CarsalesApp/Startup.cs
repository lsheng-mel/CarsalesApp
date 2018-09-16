using AutoMapper;
using CarsalesApp.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarsalesApp
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
            // we are using in memory database during the development & testing, but alternatively we could use SQL Server
            // Also we could run entity framework migration program that are included in the Migrations project to restore the database snapshot in SQL Server in deployment

            //var connectionString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
            //services.AddDbContext<VehicleContext>(opt =>
            //    opt.UseSqlServer(connectionString));

            // register DB contexts
            services.AddDbContext<VehicleContext>(opt => opt.UseInMemoryDatabase("VehicleSaleDatabase"));

            // register automapper
            services.AddAutoMapper();

            // register logger
            services.AddLogging();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
