using Loan.Domain;

namespace Loan.Service.Api.PortAdapters.ExternalServices;

public class DebtorRegistryClient : IDebtorRegistry
{
    private readonly ILogger _logger;
    public DebtorRegistryClient(ILogger<DebtorRegistryClient> logger)
    {
        _logger = logger;
    }
    private const string DebtorNationalIdentifier = "11111111111";
    public bool HasDebtor(Customer customer)
    {
        //still fake...
        //external dto's ==> convert to our model needs
        _logger.LogInformation($"Inquerying debtor service is for :{customer.Name} ");
        return customer.NationalIdentifier != new NationalIdentifier(DebtorNationalIdentifier);
    }
}