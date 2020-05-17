using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class LoanApplication  : Entity<LoanApplicationId>
    {
        public LoanApplicationNumber Number { get; }
        public LoanApplicationStatus Status { get; }
        public ScoreResult Score { get; private set; }
        public Customer Customer { get; }
        public Property Property { get; }
        public Loan Loan { get; }
        public Registration Registration { get; }
        public Decision Decision { get; }

        protected LoanApplication()
        {
        }
        public LoanApplication(LoanApplicationNumber number, Customer customer, 
            Property property, Loan loan,OperatorId registeredBy)
        {
            if (number==null)
                throw new ArgumentException("Number cannot be null");
            if (customer==null)
                throw new ArgumentException("Customer cannot be null");
            if (property==null)
                throw new ArgumentException("Property cannot be null");
            if (loan==null)
                throw new ArgumentException("Loan cannot be null");
            if (registeredBy==null)
                throw new ArgumentException("Registration cannot be null");

            Number = number;
            Customer = customer;
            Property = property;
            Loan = loan;
            
            Id = new LoanApplicationId(Guid.NewGuid());
            Status = LoanApplicationStatus.New;
            Registration = new Registration(SystemTime.Now(),registeredBy);
            Decision = null;
            Score = null;
        }
        
    }

    public class LoanApplicationId : IdentityBase<Guid>
    {
        public LoanApplicationId(Guid value):base(value)
        {
        }
        protected LoanApplicationId()
        {
        }
    }

    public class LoanApplicationNumber : ValueObject<LoanApplicationNumber>
    {
        public string Number { get;  }

        public LoanApplicationNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Loan application number cannot be null or empty string");
            Number = number;
        }
        
        public static LoanApplicationNumber NewNumber => new LoanApplicationNumber(Guid.NewGuid().ToString());
        
        public static LoanApplicationNumber Of(string number) => new LoanApplicationNumber(number);

        public static implicit operator string(LoanApplicationNumber number) => number.Number;
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Number;
        }
    }

    public enum LoanApplicationStatus
    {
        New,
        Accepted,
        Rejected
    }

    public enum ApplicationScore
    {
        Red,
        Green
    }
    
}