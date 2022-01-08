namespace Loan.Domain.Test.Builders
{
    internal class LoanBuilder
    {
        private Money amount = new Money(200_000M);
        private int numberOfYears = 20;
        private Percent interestRate = 1.Percent();

        public LoanBuilder WithAmount(decimal loanAmount)
        {
            amount = new Money(loanAmount);
            return this;
        }

        public LoanBuilder WithNumberOfYears(int numOfYears)
        {
            numberOfYears = numOfYears;
            return this;
        }

        public LoanBuilder WithInterestRate(decimal rate)
        {
            interestRate = new Percent(rate);
            return this;
        }

        public Loan Build()
        {
            return new Loan(amount, numberOfYears, interestRate);

        }
    }
}