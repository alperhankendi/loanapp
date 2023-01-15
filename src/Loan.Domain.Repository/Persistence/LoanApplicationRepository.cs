using System.Linq;

namespace Loan.Domain.Repository.Persistence;
public class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly LoanDbContext dbContext;

    public LoanApplicationRepository(LoanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void Add(LoanApplication loanApplication)
    {
        dbContext.LoanApplications.Add(loanApplication);
    }

    public LoanApplication WithNumber(LoanApplicationNumber loanApplicationNumber)
    {
        return dbContext.LoanApplications.FirstOrDefault(l => l.Number == loanApplicationNumber);
    }
}
