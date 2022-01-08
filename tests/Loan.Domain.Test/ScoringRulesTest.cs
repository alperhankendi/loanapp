using System.Linq.Expressions;
using Loan.Domain.Test.Builders;
using Xunit;

namespace Loan.Domain.Test
{
    public class ScoringRulesTest
    {
        private readonly ScoringRulesFactory scoringRulesFactory = new ScoringRulesFactory(new DebtorRegistryMock());
        
        [Fact]
        public void PropertyValueHigherThanLoanAmountMustBeLowerThanPropertyValue_IsSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithProperty(p => p.WithValue(750_000M))
                .WithLoan(l => l.WithAmount(700_000M))
                .Build();
            
            var rule = new LoanAmountMustBeLowerThanPropertyValue();
            var ruleResult = rule.IsSatisfiedBy(application);

            Assert.True(ruleResult);
        }
        [Fact]
        public void PropertyValueHigherThanLoanAmountMustBeLowerThanPropertyValue_IsNotSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithProperty(p => p.WithValue(750_000M))
                .WithLoan(l => l.WithAmount(900_000M))
                .Build();
            
            var rule = new LoanAmountMustBeLowerThanPropertyValue();
            var ruleResult = rule.IsSatisfiedBy(application);

            Assert.False(ruleResult);
        }

        [Fact]
        public void CustomerNotOlderThan65_CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65_IsSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(40))
                .WithLoan(l => l.WithNumberOfYears(20))
                .Build();
            var rule = new CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65();
            var ruleResult = rule.IsSatisfiedBy(application);
            Assert.True(ruleResult);
        }
        [Fact]
        public void CustomerNotOlderThan65_CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65_IsNotSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(40))
                .WithLoan(l => l.WithNumberOfYears(26))
                .Build();
            var rule = new CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65();
            var ruleResult = rule.IsSatisfiedBy(application);
            Assert.False(ruleResult);
        }

        [Fact]
        public void CustomerIncome15PercentHigherThenOfInstallment_IsSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithLoan(l => l.WithAmount(400_000M).WithNumberOfYears(10).WithInterestRate(1M))
                .WithCustomer(c => c.WithIncome(25_000M))
                .Build();

            var rule = new InstallmentAmountMustBeLowerThan15PercentOfCustomerIncome();
            var ruleResult = rule.IsSatisfiedBy(application);
            Assert.True(ruleResult);
        }
        [Fact]
        public void CustomerIncome15PercentHigherThenOfInstallment_IsNotSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithLoan(l => l.WithAmount(400_000M).WithNumberOfYears(10).WithInterestRate(1M))
                .WithCustomer(c => c.WithIncome(55_000M))
                .Build();

            var rule = new InstallmentAmountMustBeLowerThan15PercentOfCustomerIncome();
            var ruleResult = rule.IsSatisfiedBy(application);
            Assert.True(ruleResult);
        }

        [Fact]
        public void CustomerHasNotDebtorRecord_IsSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithIdentifier(DebtorRegistryMock.DebtorNationalIdentifier))
                .Build();
            var rule = new CustomerHasNotDebtorRecord(new DebtorRegistryMock());
            var ruleResult = rule.IsSatisfiedBy(application);
            Assert.True(ruleResult);
        }
        [Fact]
        public void CustomerHasNotDebtorRecord_IsNotSatisfied()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithIdentifier("12345678901"))
                .Build();
            var rule = new CustomerHasNotDebtorRecord(new DebtorRegistryMock());
            var ruleResult = rule.IsSatisfiedBy(application);
            Assert.False(ruleResult);
        }

        [Fact]
        public void WhenAllRulesAreSatisfied_ScoringResult_IsGreen()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(25_000M))
                .WithLoan(l => l.WithAmount(200_000M).WithNumberOfYears(25).WithInterestRate(1M))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();
            
            var scoringRules = scoringRulesFactory.DefaultSet.Evaluate(application);
            Assert.True(scoringRules.IsGreen());
        }
        [Fact]
        public void WhenAnyRulesIsNotSatisfied_ScoringResult_IsRed()
        {
            var application = new LoanApplicationBuilder()
                .WithCustomer(c => c.WithAge(25).WithIncome(5_000M))
                .WithLoan(l => l.WithAmount(200_000M).WithNumberOfYears(15).WithInterestRate(1M))
                .WithProperty(p => p.WithValue(250_000M))
                .Build();
            
            var scoringRules = scoringRulesFactory.DefaultSet.Evaluate(application);
            Assert.True(scoringRules.IsRed());
        }
    }
}