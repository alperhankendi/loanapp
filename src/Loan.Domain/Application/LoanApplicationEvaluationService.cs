using Loan.Core;

namespace Loan.Domain.Application
{
    public class LoanApplicationEvaluationService
    {
        private readonly ILoanApplicationRepository loanApplicationRepository;
        private readonly ScoringRulesFactory scoringRulesFactory;
        private readonly IUnitOfWork unitOfWork;

        public LoanApplicationEvaluationService(ILoanApplicationRepository loanApplicationRepository,
            ScoringRulesFactory scoringRulesFactory,IUnitOfWork unitOfWork)
        {
            this.loanApplicationRepository = loanApplicationRepository;
            this.scoringRulesFactory = scoringRulesFactory;
            this.unitOfWork = unitOfWork;
        }
        
        public void EvaluateLoanApplication(string applicationNumber)
        {
            var application = loanApplicationRepository.WithNumber(LoanApplicationNumber.Of(applicationNumber));
            
            if (application == null)
                throw new LoanApplicationNotFound($"Loan Application ({applicationNumber}) not found");
            
            application.Evaluate(scoringRulesFactory.DefaultSet);
            unitOfWork.CommitChanges();
        }
    }
}