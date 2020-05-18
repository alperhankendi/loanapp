namespace Loan.Domain.Services
{
    public class LoanApplicationSubmissionService
    {
        private readonly IOperatorRepository operatorRepository;
        private readonly ILoanApplicationRepository loanApplicationRepository;

        public LoanApplicationSubmissionService(IOperatorRepository operatorRepository,
            ILoanApplicationRepository loanApplicationRepository)
        {
            this.operatorRepository = operatorRepository;
            this.loanApplicationRepository = loanApplicationRepository;
        }
        
        public LoanApplicationNumber SubmitLoanApplication(LoanApplication loanApplication, string login)
        {
            var user = operatorRepository.WithLogin(Login.Of(login));
            
            return LoanApplicationNumber.Of("11111111");
        }
    }
}