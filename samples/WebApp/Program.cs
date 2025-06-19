using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog.Events;

namespace WebApp
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Serilog;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog() // sử dụng Serilog cho ASP.NET Core
                .Build();
    }

}
