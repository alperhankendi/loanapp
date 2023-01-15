
using Loan.Core;
using Loan.Domain;
using Microsoft.AspNetCore.Mvc;
using Loan.Service.Api.PortAdapters.ExternalServices;
using Loan.Service.Api;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHealthChecks().AddDbContextCheck<Loan.Domain.Repository.Persistence.LoanDbContext>();
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);


        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainServices(configuration.GetConnectionString("DefaultConnection"));
        services.AddSingleton<IDebtorRegistry, DebtorRegistryClient>();
        services.AddSingleton<IEmailSender, EmailSender>();
        services.UseServiceBus();
        return services;
    }
}
