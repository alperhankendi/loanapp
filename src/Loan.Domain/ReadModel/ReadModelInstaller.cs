using Loan.Domain.ReadModel;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ReadModelInstaller
    {
        public static void AddReadModelServices(this IServiceCollection services, string connString)
        {
            services.AddSingleton(_ => new LoanApplicationFinder(connString));
        }
    }
}