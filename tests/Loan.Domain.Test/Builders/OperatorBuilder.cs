namespace Loan.Domain.Test.Builders
{
    internal class OperatorBuilder
    {
        private string login = "admin";
        private string password = "admin";
        private decimal competenceLevel = 1_000_000M;

        public OperatorBuilder WithLogin(string log)
        {
            login = log;
            return this;
        }

        public OperatorBuilder WithCompentenceLevel(decimal amount)
        {
            competenceLevel = amount;
            return this;
        }
        
        public Operator Build()
        {
            return new Operator(Login.Of(login),Password.Of(password),
                new Name(login,login),new MonetaryAmount(competenceLevel)   );
        }
    }
}