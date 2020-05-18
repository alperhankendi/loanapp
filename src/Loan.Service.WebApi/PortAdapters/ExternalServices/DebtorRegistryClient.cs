using Loan.Domain;

namespace Loan.Service.WebApi.PortAdapters.ExternalServices
{
    public class DebtorRegistryClient : IDebtorRegistry
    {
        private const string DebtorNationalIdentifier = "11111111111";
        public bool HasDebtor(Customer customer)
        {
            //still fake...
            return customer.NationalIdentifier == new NationalIdentifier(DebtorNationalIdentifier);
        }
    }
}