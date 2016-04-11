using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IoF_Admin.Services;
using IoF_Admin.Services.Implementations;
using IoF_Admin.Services.Fakes;
using IoF_Admin.Models;
using Microsoft.Data.Entity;
using System.IO;

namespace IoF_Admin
{
    public class Startup
    {
        private string path = "";

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();

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
            try
            {
                databaseFilePath = Path.Combine(this.path, databaseFilePath);                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error configuraing database: {0}", ex.Message);
                databaseFilePath = ".\\wwwroot\\" + databaseFilePath;
            }

            Console.WriteLine("Creating database: {0}", databaseFilePath);
            var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = databaseFilePath };
            var connectionString = connectionStringBuilder.ToString();

            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<IoFContext>(options => options.UseSqlite(connectionString));

            // Register application services.
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IAquariumService, AquariumService>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);

            // Register application services.
            services.AddTransient<IConfigurationService, FakeConfigurationService>();
            services.AddTransient<IAquariumService, FakeAquariumService>();
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

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Aquarium}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
