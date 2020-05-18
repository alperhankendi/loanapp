using Loan.Domain.Application;
using Loan.Domain.Test.Builders;
using Xunit;
using Xunit.Abstractions;

namespace Loan.Domain.Test
{
    public class LoanApplicationEvaluationServiceTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public LoanApplicationEvaluationServiceTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public void LoanApplicationEvaluationService_ApplicationThatSatisfiesAllRules_IsEvaluatedGreen()
        {
            var existingApplication = new LoanApplicationBuilder()
                .WithNumber("123")
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();

            var loanApplicationRepositoryMock = new LoanApplicationRepositoryMock(new []{existingApplication});

            var evaluationService = new LoanApplicationEvaluationService(loanApplicationRepositoryMock,
                new ScoringRulesFactory(new DebtorRegistryMock()),
                new UnitOfWorkMock()
            );
            
            evaluationService.EvaluateLoanApplication(LoanApplicationNumber.Of("123"));
            existingApplication.ScoreIs(ApplicationScore.Green);
        }
        [Fact]
        public void LoanApplicationEvaluationService_ApplicationThatDoesNotSatifiedAllrules_IsEvaluatedRedAndRejected()
        {
            var existingApplication = new LoanApplicationBuilder()
                .WithNumber("123")
                .WithCustomer(c => c.WithAge(55).WithIncome(2_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();

            var loanApplicationRepositoryMock = new LoanApplicationRepositoryMock(new []{existingApplication});

            var evaluationService = new LoanApplicationEvaluationService(loanApplicationRepositoryMock,
                new ScoringRulesFactory(new DebtorRegistryMock()),
                new UnitOfWorkMock()
            );
            
            evaluationService.EvaluateLoanApplication(LoanApplicationNumber.Of("123"));
            existingApplication.ScoreIs(ApplicationScore.Red).IsInStatus(LoanApplicationStatus.Rejected);
            testOutputHelper.WriteLine($"Reject reasons: {existingApplication.Score.Explanation}");
        }
        
    }
}