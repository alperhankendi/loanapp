using System.Collections.Generic;
using Loan.Domain.Application;
using Loan.Domain.Test.Builders;
using Xunit;
using Xunit.Abstractions;

namespace Loan.Domain.Test
{
    public class LoanApplicationDecisionServiceTest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public LoanApplicationDecisionServiceTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void LoanApplicationDecisionService_GreenApplication_CanBeAccepted()
        {
            var operators = new OperatorRepositoryMock(new List<Operator>
            {
                new OperatorBuilder().WithLogin("admin").Build(),
            });
            var existingApplication = new LoanApplicationBuilder()
                .WithNumber("123")
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Build();

            var eventPublisher = new EventPublisherMock();

            var decisionService = new LoanApplicationDecisionService(
                new UnitOfWorkMock(), 
                new LoanApplicationRepositoryMock(new []{existingApplication}),
                operators,
                eventPublisher
            );
            
            decisionService.AcceptApplication("123","admin");
            existingApplication.IsInStatus(LoanApplicationStatus.Accepted);
            Assert.Single(eventPublisher.Events);
            Assert.Contains(eventPublisher.Events,
                e => e.GetType() == typeof(Events.V1.LoanApplicationAccepted)
                     && ((Events.V1.LoanApplicationAccepted) e).LoanApplicationId == existingApplication.Id.Id);
        }
        [Fact]
        public void LoanApplicationDecisionService_GreenApplication_CanBeRejected()
        {
            var operators = new OperatorRepositoryMock(new List<Operator>
            {
                new OperatorBuilder().WithLogin("admin").Build(),
            });
            var existingApplication = new LoanApplicationBuilder()
                .WithNumber("123")
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Build();
            var eventPublisher = new EventPublisherMock();
            var decisionService = new LoanApplicationDecisionService(
                new UnitOfWorkMock(), 
                new LoanApplicationRepositoryMock(new []{existingApplication}),
                operators,
                eventPublisher
            );
            
            decisionService.RejectApplication("123","admin");
            existingApplication.IsInStatus(LoanApplicationStatus.Rejected);
            Assert.Single(eventPublisher.Events);
            Assert.Contains(eventPublisher.Events,
                e => e.GetType() == typeof(Events.V1.LoanApplicationRejected)
                     && ((Events.V1.LoanApplicationRejected) e).LoanApplicationId == existingApplication.Id.Id);

        }
    }
}