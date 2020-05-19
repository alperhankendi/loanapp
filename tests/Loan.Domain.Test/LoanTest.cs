using Loan.Domain.Test.Builders;
using Xunit;

namespace Loan.Domain.Test
{
    public class LoanTest
    {
        [Fact]
        public void Can_Calculate_Monthly_installment()
        {
            var loan = new LoanBuilder()
                .WithAmount(420_000M)
                .WithNumberOfYears(3)
                .WithInterestRate(5M)
                .Build();
            
            var monthlyInstallment = loan.MonthlyInstallment();
            Assert.Equal(new Money(12_587.78M), monthlyInstallment);
        }
    }
}