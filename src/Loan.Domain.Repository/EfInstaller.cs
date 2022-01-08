using Loan.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.Domain.Repository
{
    public static class EfInstaller
    {
        public static void AddEfDbAdapters(this IServiceCollection services, string connString)
        {
            services.AddDbContext<LoanDbContext>(opts =>
            {
                opts.UseNpgsql(connString);
            });
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddHostedService<EfDbInitializer>();
        }
    }
}