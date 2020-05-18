using Loan.Domain;
using Loan.Domain.Application;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServicesInstaller
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ScoringRulesFactory>();
            services.AddScoped<LoanApplicationSubmissionService>();
            services.AddScoped<LoanApplicationEvaluationService>();
            services.AddScoped<LoanApplicationDecisionService>();
        }
    }
}