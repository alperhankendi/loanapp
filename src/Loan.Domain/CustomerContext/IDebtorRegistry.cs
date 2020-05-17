namespace Loan.Domain
{
    public interface IDebtorRegistry
    {
        bool HasDebtor(Customer customer);
    }
}