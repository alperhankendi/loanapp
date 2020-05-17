using Loan.Domain.Test.Builders;
using Xunit;

namespace Loan.Domain.Test
{
    public class LoanApplicationTest
    {
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
    }
    
}