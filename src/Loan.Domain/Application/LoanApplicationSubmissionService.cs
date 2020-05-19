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
                        new Money(applicationDto.MonthlyIncome),
                        new Address(applicationDto.CustomerAddress.Country,
                                    applicationDto.CustomerAddress.ZipCode,
                                    applicationDto.CustomerAddress.City,
                                    applicationDto.CustomerAddress.Street)
                    ),
                    new Property(new Money(applicationDto.PropertyValue),
                        new Address(applicationDto.ProperyAddress.Country,
                            applicationDto.ProperyAddress.ZipCode,
                            applicationDto.ProperyAddress.City,
                            applicationDto.ProperyAddress.Street)),
                    new Loan(new Money(applicationDto.LoanAmount),
                        applicationDto.LoanNumberOfYears,
                        new Percent(applicationDto.LoanInterestRate)),     
                    user.Id
                    );
            
            loanApplicationRepository.Add(application);
            unitOfWork.CommitChanges();
            return application.Number;
        }
    }
}