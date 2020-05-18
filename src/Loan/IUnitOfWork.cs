namespace Loan.Core
{
    public interface IUnitOfWork
    {
        void CommitChanges();
    }
}