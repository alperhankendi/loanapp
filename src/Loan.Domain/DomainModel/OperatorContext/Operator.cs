using System;
using Loan.Core;

namespace Loan.Domain
{
    public class Operator : Entity<OperatorId>
    {
        protected Operator()
        {
        }
        public Operator(Login login, Password password, Name name, MonetaryAmount competenceLevel)
        {
            if (competenceLevel==null)
                throw new ArgumentException("CompetenceLevel cannot be null");
            if (competenceLevel==MonetaryAmount.Zero)
                throw new ArgumentException("CompetenceLevel must be greater than 0");

            Id = new OperatorId(Guid.NewGuid());
            Login = login;
            Password = password;
            Name = name;
            CompetenceLevel = competenceLevel;
        }

        public Login Login { get; }
        public Password Password { get; }
        public Name   Name { get; }
        public MonetaryAmount CompetenceLevel { get; }

        public bool CanAccept(MonetaryAmount loanAmount) => loanAmount < CompetenceLevel;
    }

    public class OperatorId : IdentityBase<Guid>
    {
        public OperatorId(Guid id) : base(id)
        {
        }
    }
}