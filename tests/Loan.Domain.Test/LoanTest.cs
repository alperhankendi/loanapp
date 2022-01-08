using Loan.Domain.Test.Builders;
using Xunit;

namespace Loan.Domain.Test
{
    public class LoanTest
    {
        [Theory]
        [InlineData(420000,3,5,12587.78)]
        [InlineData(100000,3,1,2820.81)]
        public void Can_Calculate_Monthly_installment(decimal amount,int year,decimal rate,decimal excepted)
        {
            var loan = new LoanBuilder()
                .WithAmount(amount)
                .WithNumberOfYears(year)
                .WithInterestRate(rate)
                .Build();
            
            var monthlyInstallment = loan.MonthlyInstallment();
            Assert.Equal(new Money(excepted), monthlyInstallment);
        }
    }
}