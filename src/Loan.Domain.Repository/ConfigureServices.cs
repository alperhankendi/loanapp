using Loan.Core;
using Loan.Domain;
using Loan.Domain.Repository.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<LoanDbContext>(opts =>
            {
                opts.UseSqlite($"Data Source={System.IO.Path.Join(@"C:\workspace\loanapp\", "loan_ddd.db")}");
            });
        }
        else
        {
            services.AddDbContext<LoanDbContext>(opts =>
            {
                opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),builder =>{});
            });
        }

        services.AddScoped<LoanDbContext>();
        services.AddScoped<LoanDbContextInitialiser>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
        services.AddScoped<IOperatorRepository, OperatorRepository>();

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var initialiser = scope.ServiceProvider.GetRequiredService<LoanDbContextInitialiser>();
            initialiser.InitialiseAsync().GetAwaiter();
            initialiser.SeedAsync(CancellationToken.None).GetAwaiter();
        }

        return services;
    }
}