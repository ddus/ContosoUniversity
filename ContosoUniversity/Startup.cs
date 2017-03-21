using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights.Extensibility;

using NLog.Extensions.Logging;
using NLog.Web;

using ContosoUniversity.Models;

namespace ContosoUniversity
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      TelemetryConfiguration.Active.DisableTelemetry = true;

      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
      env.ConfigureNLog("nlog.config");
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<SchoolContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      //needed for NLog.Web
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      // Add framework services.
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
        ILoggerFactory loggerFactory, SchoolContext context)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug((category, logLevel) => 
        {return category.StartsWith("Microsoft.EntityFrameworkCore");});

      NLogProviderOptions options = new NLogProviderOptions();
      loggerFactory.AddNLog(options);
      //loggerFactory.CreateLogger("Microsoft.EntityFrameworkCore");

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
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
      SchoolInitializer.Initialize(context);
    }
  }
}
