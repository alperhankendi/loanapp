namespace Loan.Domain.Test
{
    public class DebtorRegistryMock : IDebtorRegistry
    {
        public const string DebtorNationalIdentifier = "11111111111";
        public bool HasDebtor(Customer customer)
        {
            return customer.NationalIdentifier == new NationalIdentifier(DebtorNationalIdentifier);
        }
    }
}