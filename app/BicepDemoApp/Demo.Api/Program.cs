using Demo.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Demo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static IConfigurationRoot _config;

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    _config = config.Build();
                })
                .ConfigureLogging(logging =>
                {
                    var appInsightsInstrumentationKey = _config["APPINSIGHTS_INSTRUMENTATIONKEY"];
                    var logLevel = _config.GetValue("Logging:Loglevel:Default", LogLevel.Warning);

                    logging.ClearProviders();

                    // Providing an instrumentation key here is required if you are using
                    // standalone package Microsoft.Extensions.Logging.ApplicationInsights
                    // or if you want to capture logs from early in the application startup
                    // pipeline from Startup.cs or Program.cs itself.
                    logging.AddApplicationInsights(appInsightsInstrumentationKey);
                    logging.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights
                        .ApplicationInsightsLoggerProvider>("", logLevel);
                });

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ExerciseAppContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
