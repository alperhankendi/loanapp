using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class LoanApplication  : Entity<LoanApplicationId>
    {
        public LoanApplicationNumber Number { get; }
        public LoanApplicationStatus Status { get; private set; }
        public ScoreResult Score { get; private set; }
        public Customer Customer { get; }
        public Property Property { get; }
        public Loan Loan { get; }
        public Registration Registration { get; }
        public Decision Decision { get; }        
        
    }

    public class LoanApplicationId : ValueObject<LoanApplicationId>
    {
        public Guid Value { get; }
        public LoanApplicationId(Guid value)
        {
            Value = value;
        }
        protected LoanApplicationId()
        {
            
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
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