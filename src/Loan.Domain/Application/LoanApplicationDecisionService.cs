using Loan.Core;

namespace Loan.Domain.Application
{
    public class LoanApplicationDecisionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoanApplicationRepository loanApplicationRepository;
        private readonly IOperatorRepository operatorRepository;
        private readonly IEventPublisher eventPublisher;

        public LoanApplicationDecisionService(IUnitOfWork unitOfWork,
            ILoanApplicationRepository loanApplicationRepository,IOperatorRepository operatorRepository,
            IEventPublisher eventPublisher
            )
        {
            this.unitOfWork = unitOfWork;
            this.loanApplicationRepository = loanApplicationRepository;
            this.operatorRepository = operatorRepository;
            this.eventPublisher = eventPublisher;
        }
        public void RejectApplication(string applicationNumber, string login)
        {
            var @operator = operatorRepository.WithLogin(Login.Of(login));
            var application = loanApplicationRepository.WithNumber(LoanApplicationNumber.Of(applicationNumber));
            
            application.Reject(@operator);
            unitOfWork.CommitChanges();
            
            eventPublisher.Publish(new Events.V1.LoanApplicationRejected(application));
        }

        public void AcceptApplication(string applicationNumber, string login)
        {
            var @operator = operatorRepository.WithLogin(Login.Of(login));
            var application = loanApplicationRepository.WithNumber(LoanApplicationNumber.Of(applicationNumber));
            
            application.Accept(@operator);
            unitOfWork.CommitChanges();
            
            eventPublisher.Publish<Events.V1.LoanApplicationAccepted>(new Events.V1.LoanApplicationAccepted(application));
        }
    }
}