namespace Loan.Domain
{
    public interface ILoanApplicationRepository
    {
        void Add(LoanApplication loanApplication);

        LoanApplication WithNumber(LoanApplicationNumber loanApplicationNumber);
    }
}