using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace Ecommerce.Services.Orders.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using IHost host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                if (Log.Logger == null ||
					Log.Logger.GetType().Name == "SilentLogger")
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .CreateLogger();
                }

                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder
						.AddConfigServer()
                        .UseStartup<Startup>()
                        .CaptureStartupErrors(true)
                        .ConfigureAppConfiguration(config =>
                        {
                            var prefix = Assembly
                                .GetEntryAssembly()
                                .GetName()
                                .Name
                                .Replace(".", "_") + "_";

                            config.AddEnvironmentVariables(prefix: prefix);
                        })
                        .UseSerilog((hostingContext, loggerConfiguration) =>
                        {
                            loggerConfiguration.ReadFrom
                                .Configuration(hostingContext.Configuration);
#if DEBUG
                            loggerConfiguration.Enrich
                                .WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif
                        });
                });
    }
}
