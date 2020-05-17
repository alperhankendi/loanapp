using Loan.Domain.Test.Builders;
using Xunit;

namespace Loan.Domain.Test
{
    public class LoanApplicationTest
    {
        private readonly ScoringRulesFactory scoringRulesFactory = new ScoringRulesFactory(new DebtorRegistryMock());

        [Fact]
        public void NewApplication_IsCreatedIn_NewStatus_AndNullScore()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(customer => customer.WithAge(25).WithIncome(15_000M))
                .WithLoan(loan => loan.WithAmount(200_000).WithNumberOfYears(25).WithInterestRate(1.1M))
                .WithProperty(property => property.WithValue(250_000M))
                .Build();

            application.
                IsInStatus(LoanApplicationStatus.New).
                IsScoreNull();
        }

        [Fact]
        public void ValidApplication_EvaluationScore_IsGreen()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();
            
            application.Evaluate(scoringRulesFactory.DefaultSet);
            application
                .IsInStatus(LoanApplicationStatus.New)
                .ScoreIs(ApplicationScore.Green);
        }
        [Fact]
        public void InvalidApplication_EvaluationScore_IsRedAndStatusIsRejected()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(55).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(300_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();
            
            application.Evaluate(scoringRulesFactory.DefaultSet);
            application
                .IsInStatus(LoanApplicationStatus.Rejected)
                .ScoreIs(ApplicationScore.Red);
        }

        [Fact]
        public void LoanApplication_CanBeAccepted()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Build();

            var user = new OperatorBuilder().Build();
            application.Accept(user);
          
            application
                .IsInStatus(LoanApplicationStatus.Accepted)
                .ScoreIs(ApplicationScore.Green);
        }
        [Fact]
        public void LoanApplication_WithoutScore_CannotBeAccepted()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();

            var user = new OperatorBuilder().Build();
            var ex = Assert.Throws<NeedToScoreApplicationException>(() => application.Accept(user));
          
            Assert.IsType<NeedToScoreApplicationException>(ex);
        }

        [Fact]
        public void LoanApplication_InStatusNew_EvaluatedGreen_OperatorHasCompetenceLevel_CanBeAccepted()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Build();
            var user = new OperatorBuilder().WithCompentenceLevel(500_000M).Build();
            
            application.Accept(user);

            application
                .IsInStatus(LoanApplicationStatus.Accepted)
                .ScoreIs(ApplicationScore.Green);

        }
        [Fact]
        public void LoanApplication_InStatusNew_EvaluatedGreen_OperatorDoesNotHaveCompetenceLevel_CanNotBeAccepted()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Build();
            
            var user = new OperatorBuilder().WithCompentenceLevel(100_000M).Build();

            var ex = Assert.Throws<OperatorDoesNotHaveRequiredCompetenceLevelException>(
                () => application.Accept(user));

            Assert.IsType<OperatorDoesNotHaveRequiredCompetenceLevelException>(ex);
        }

        [Fact]
        public void LoanApplication_WithoutScore_CanBeRejected()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();

            var user = new OperatorBuilder().Build();
            application.Reject(user);

            application.IsInStatus(LoanApplicationStatus.Rejected);
        }

        [Fact]
        public void LoanApplication_InStatusNew_EvaluatedGreen_CanBeRejected()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Build();

            var user = new OperatorBuilder().Build();
            application.Reject(user);

            application.IsInStatus(LoanApplicationStatus.Rejected);     
        }            
        
        [Fact]
        public void LoanApplication_Accepted_CannotBeRejected()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Accepted()
                .Build();

            var user = new OperatorBuilder().Build();
            var ex = Assert.Throws<InvalidLoanApplicationStatusException>(() => application.Reject(user));
          
            Assert.IsType<InvalidLoanApplicationStatusException>(ex);
        }
        [Fact]
        public void LoanApplication_Rejected_CannotBeAccepted()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(20_000M))
                .WithLoan(l => l.WithAmount(200_000).WithInterestRate(1.1M).WithNumberOfYears(20))
                .WithProperty(p => p.WithValue(250_000M))
                .Evaluated()
                .Rejected()
                .Build();

            var user = new OperatorBuilder().Build();
            var ex = Assert.Throws<InvalidLoanApplicationStatusException>(() => application.Accept(user));
          
            Assert.IsType<InvalidLoanApplicationStatusException>(ex);
        }
        
    }

    public static class LoanApplicationTestAssert
    {
        public static LoanApplication IsInStatus(this LoanApplication application, LoanApplicationStatus exceptedStatus)
        {
            Assert.Equal(exceptedStatus,application.Status);
            return application;
        }

        public static LoanApplication IsScoreNull(this LoanApplication application)
        {
            Assert.Null(application.Score);
            return application;
        }
        public static LoanApplication ScoreIs(this LoanApplication application,ApplicationScore score)
        {
            Assert.Equal(score,application.Score?.Score);
            return application;
        }
    }
    
}