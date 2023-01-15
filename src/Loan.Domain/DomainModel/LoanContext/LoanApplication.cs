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
        public Customer Customer { get; private set; }
        public Property Property { get; private set; }
        public Loan Loan { get; }
        public Registration Registration { get; }
        public Decision Decision { get; private set; }

        protected LoanApplication()
        {
        }
        public LoanApplication(LoanApplicationNumber number, Customer customer, 
            Property property, Loan loan)
        {
            if (number==null)
                throw new ArgumentException("Number cannot be null");
            if (customer==null)
                throw new ArgumentException("Customer cannot be null");
            if (property==null)
                throw new ArgumentException("Property cannot be null");
            if (loan==null)
                throw new ArgumentException("Loan cannot be null");

            Number = number;
            Customer = customer;
            Property = property;
            Loan = loan;
            
            Id = new LoanApplicationId(Guid.NewGuid());
            Status = LoanApplicationStatus.New;
            Registration = new Registration(SystemTime.Now(),null);
            Decision = null;
            Score = null;
        }

        public void Evaluate(ScoringRules rules)
        {
            Score = rules.Evaluate(this);
            if (Score.IsRed())
            {
                Status = LoanApplicationStatus.Rejected;
            }
        }

        //Status should be New => Accept or Reject
        public void Accept(Operator decisionBy)
        {
            if (this.Status != LoanApplicationStatus.New)
                throw new InvalidLoanApplicationStatusException($"Cannot accept application. The Status is already changed. The current status of application is {Status} And the Reason: {Score.Explanation}");
            
            if (Score == null)
                throw new NeedToScoreApplicationException($"Cannot accept application before scoring");

            if (!decisionBy.CanAccept(this.Loan.LoanAmount))
                throw new OperatorDoesNotHaveRequiredCompetenceLevelException($"Operator does not required competence level to accept application");

            Status = LoanApplicationStatus.Accepted;
            Decision = new Decision(SystemTime.Now(),decisionBy.Id);
        }

        public void Reject(Operator decisionBy)
        {
            if (this.Status != LoanApplicationStatus.New)
                throw new InvalidLoanApplicationStatusException($"Cannot reject application. The Status is already changed. The current status of application is {Status} And the Reason: {Score.Explanation}");

            Status = LoanApplicationStatus.Rejected;
            Decision = new Decision(SystemTime.Now(),decisionBy.Id);
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