using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Yotodo.Services;

using Yotodo.Data;

namespace Yotodo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddEntityFramework()
                .AddNpgsql()
                .AddDbContext<ApplicationDataContext>(options => options.UseNpgsql(Configuration["Data:ConnectionString"]));
            
            // Add framework services.
             services.AddMvc(options =>
            {
                var jsonOutputFormatter = new JsonOutputFormatter
                {
                    SerializerSettings =
                    {   
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat
                    }
                };
                jsonOutputFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

                options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                options.OutputFormatters.Insert(0, jsonOutputFormatter);
            });
            
            
            services.AddTransient<IStatisticsService, StatisticsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
