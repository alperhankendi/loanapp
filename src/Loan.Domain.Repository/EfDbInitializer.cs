using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Loan.Domain.Repository
{
    public class EfDbInitializer : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public EfDbInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var dbCtx = scope.ServiceProvider.GetService<LoanDbContext>();

            if (!dbCtx.Operators.Any(o => o.Login == new Login("admin")))
            {
                await dbCtx.Operators.AddAsync(new Operator(
                    new Login("admin"),new Password("admin"), 
                    new Name("Admin","Admin"),
                    new MonetaryAmount(1_000_000M)  
                ), cancellationToken);
            }

            await dbCtx.SaveChangesAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}