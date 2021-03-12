using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Environment;


namespace Loan.Service.WebApi
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            await Console.Error.WriteLineAsync($"Application starting ({GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"})...");

            var configuration = BuildConfiguration(args);
            await Console.Error.WriteLineAsync("Configuration built successfully.");
            await CreateHostBuilder(args)
                .ConfigureWebHost(s=>
                {
                    s.UseConfiguration(configuration);
                })
                .ConfigureServices(services => services.AddSingleton(configuration))
                .Build()
                .RunAsync();

            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); 
                    
                });
        
        private static IConfiguration BuildConfiguration(string[] args)
            => new ConfigurationBuilder()
                .SetBasePath(CurrentDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .AddJsonFile($"appsettings.{GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        
    }
}