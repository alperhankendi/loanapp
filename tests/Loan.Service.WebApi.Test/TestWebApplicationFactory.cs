using Loan.Domain.Repository.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Loan.Service.WebApi.Test;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LoanDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<LoanDbContext>(options =>
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                options.UseSqlite($"Data Source={Path.Join(path, "loan_ddd_tests1.db")}");
            });
            
        });

        Console.WriteLine("burda");
        builder.ConfigureServices(services => {
            Console.WriteLine("building service");
            using var scope = services.BuildServiceProvider().CreateScope();
            var db  = scope.ServiceProvider.GetService<LoanDbContext>();
            Console.WriteLine("migration is starting");
            db?.Database.Migrate();
            Console.WriteLine("migration done");
        });

        
        

        return base.CreateHost(builder);
    }
}