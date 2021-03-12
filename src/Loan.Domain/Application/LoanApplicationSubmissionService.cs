using Loan.Core;

namespace Loan.Domain.Application
{
    public class LoanApplicationSubmissionService
    {
        private readonly IOperatorRepository operatorRepository;
        private readonly ILoanApplicationRepository loanApplicationRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEventPublisher eventPublisher;

        public LoanApplicationSubmissionService(IOperatorRepository operatorRepository,
            ILoanApplicationRepository loanApplicationRepository,
            IUnitOfWork unitOfWork,
            IEventPublisher eventPublisher
            )
        {
            this.operatorRepository = operatorRepository;
            this.loanApplicationRepository = loanApplicationRepository;
            this.unitOfWork = unitOfWork;
            this.eventPublisher = eventPublisher;
        }
        
        public string SubmitLoanApplication(LoanApplication application)
        {
            loanApplicationRepository.Add(application);
            unitOfWork.CommitChanges();
            var msg = new Events.V1.LoanApplicationSubmitted(application);
            eventPublisher.Publish(msg);
            return application.Number;
        }
    }
}