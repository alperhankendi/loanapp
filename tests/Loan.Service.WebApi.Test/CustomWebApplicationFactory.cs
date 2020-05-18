using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.Service.WebApi.Test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var configuration = BuildConfiguration();
                services.AddOptions();
                
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .BuildServiceProvider();

                // Build the service provider.
                var sp = services.BuildServiceProvider();
            });
        }

        private static IConfiguration BuildConfiguration()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            Console.WriteLine(configPath);
            return new ConfigurationBuilder()
                //  .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(configPath, false, false)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}