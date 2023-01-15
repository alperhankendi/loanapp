using Loan.Core;

namespace Loan.Domain.Repository.Persistence;
public class EfUnitOfWork : IUnitOfWork
{
    private readonly LoanDbContext dbContext;

    public EfUnitOfWork(LoanDbContext context)
    {
        dbContext = context;
    }
    public void CommitChanges()
    {
        dbContext.SaveChanges();
    }
}