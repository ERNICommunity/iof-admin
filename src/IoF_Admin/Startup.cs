using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IoF_Admin.Services;
using IoF_Admin.Services.Implementations;
using IoF_Admin.Services.Fakes;
using IoF_Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using AutoMapper;

namespace IoF_Admin
{
    public class Startup
    {
        private MapperConfiguration mapperConfiguration { get; set; }

        private string path = "";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            Configuration = builder.Build();

            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            this.path = env.WebRootPath;
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //Add Database
            string databaseFilePath = "IoF.db";
            var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = databaseFilePath };
            var connectionString = connectionStringBuilder.ToString();
            
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<IoFContext>(options =>options.UseSqlite(connectionString));

            // Register application services.
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IAquariumService, AquariumService>();

            //Add Automapper
            services.AddSingleton<IMapper>(sp => mapperConfiguration.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);

            // Register application services.
            //services.AddTransient<IConfigurationService, FakeConfigurationService>();
            //services.AddTransient<IAquariumService, FakeAquariumService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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
