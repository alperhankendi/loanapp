using Loan.Core;

namespace Loan.Domain.Application
{
    public class LoanApplicationSubmissionService
    {
        private readonly IOperatorRepository operatorRepository;
        private readonly ILoanApplicationRepository loanApplicationRepository;
        private readonly IUnitOfWork unitOfWork;

        public LoanApplicationSubmissionService(IOperatorRepository operatorRepository,
            ILoanApplicationRepository loanApplicationRepository,
            IUnitOfWork unitOfWork
            )
        {
            this.operatorRepository = operatorRepository;
            this.loanApplicationRepository = loanApplicationRepository;
            this.unitOfWork = unitOfWork;
        }
        
        public string SubmitLoanApplication(Contract.V1.SubmitApplication applicationDto, string login)
        {
            var user = operatorRepository.WithLogin(Login.Of(login));
            
            var application = new LoanApplication(
                    LoanApplicationNumber.NewNumber, 
                    new Customer(
                        new NationalIdentifier(applicationDto.NationalIdentifier),
                        new Name(applicationDto.FirstName,applicationDto.LastName),
                        applicationDto.BirthDate,
                        new MonetaryAmount(applicationDto.MonthlyIncome),
                        new Address(applicationDto.CustomerAddress.Country,
                                    applicationDto.CustomerAddress.ZipCode,
                                    applicationDto.CustomerAddress.City,
                                    applicationDto.CustomerAddress.Street)
                    ),
                    new Property(new MonetaryAmount(applicationDto.PropertyValue),
                        new Address(applicationDto.ProperyAddress.Country,
                            applicationDto.ProperyAddress.ZipCode,
                            applicationDto.ProperyAddress.City,
                            applicationDto.ProperyAddress.Street)),
                    new Loan(new MonetaryAmount(applicationDto.LoanAmount),
                        applicationDto.LoanNumberOfYears,
                        new Percent(applicationDto.LoanInterestRate)),     
                    user.Id
                    );
            
            loanApplicationRepository.Add(application);
            unitOfWork.CommitChanges();
            return application.Number;
        }
    }

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
            
            application.Evaluate(scoringRulesFactory.DefaultSet);
            unitOfWork.CommitChanges();
        }
    }
}