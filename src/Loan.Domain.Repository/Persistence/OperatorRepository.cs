using System.Linq;
namespace Loan.Domain.Repository.Persistence;
public class OperatorRepository : IOperatorRepository
{
    private readonly LoanDbContext dbContext;

    public OperatorRepository(LoanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void Add(Operator @operator)
    {
        dbContext.Operators.Add(@operator);
    }

    public Operator WithLogin(Login login)
    {
        return dbContext.Operators.FirstOrDefault(l => l.Login == login);
    }
}