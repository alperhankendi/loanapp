using Loan.Domain.Application;
using Loan.Domain;
using Loan.Domain.ReadModel;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services,string connString)
    {
        //use cases
        services.AddSingleton<ScoringRulesFactory>();
        services.AddScoped<LoanApplicationSubmissionService>();
        services.AddScoped<LoanApplicationEvaluationService>();
        services.AddScoped<LoanApplicationDecisionService>();

        //readmodel
        services.AddSingleton(_ => new LoanApplicationFinder(connString));

        return services;
    }
}
