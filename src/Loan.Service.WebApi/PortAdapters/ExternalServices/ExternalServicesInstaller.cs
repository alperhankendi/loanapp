using Loan.Core;
using Loan.Domain;
using Loan.Service.WebApi.PortAdapters.ExternalServices;
using Microsoft.Extensions.DependencyInjection;

namespace Loan.Service.WebApi
{
    public static class ExternalServicesInstaller
    {
        public static void AddExternalServicesClients(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDebtorRegistry, DebtorRegistryClient>();
            serviceCollection.AddSingleton<IEmailSender, EmailSender>();
        }
    }
}