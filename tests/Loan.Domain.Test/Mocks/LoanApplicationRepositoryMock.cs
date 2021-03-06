using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Loan.Domain.Test
{
    public class LoanApplicationRepositoryMock : ILoanApplicationRepository
    {
        private readonly ConcurrentDictionary<LoanApplicationId,LoanApplication> loanApplications=new ConcurrentDictionary<LoanApplicationId, LoanApplication>();

        public LoanApplicationRepositoryMock(IEnumerable<LoanApplication> initalData)
        {
            if (initalData == null) return;
            foreach (var @operator in initalData)
            {
                this.loanApplications[@operator.Id] = @operator;
            }
        }

        public void Add(LoanApplication loanApplication)
        {
            this.loanApplications[loanApplication.Id] = loanApplication;
        }

        public LoanApplication WithNumber(LoanApplicationNumber loanApplicationNumber)
        {
            return this.loanApplications.Values.FirstOrDefault(o => o.Number == loanApplicationNumber);
        }
    }
}