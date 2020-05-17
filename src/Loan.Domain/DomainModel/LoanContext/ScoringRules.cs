using System;
using System.Collections.Generic;
using System.Linq;

namespace Loan.Domain
{
    public class ScoringRules
    {
        private readonly IList<SpecificationBase> _rules;

        public ScoringRules(IList<SpecificationBase> rules)
        {
            _rules = rules;
        }

        public ScoreResult Evaluate(LoanApplication loanApplication)
        {
            var brokenRules = _rules
                .Where(r => !r.IsSatisfiedBy(loanApplication)).ToList();

            return brokenRules.Any() ? 
                ScoreResult.Red(brokenRules.Select(s => s.Message).ToArray()):
                ScoreResult.Green();
        }
    }
    
    public class LoanAmountMustBeLowerThanPropertyValue : SpecificationBase
    {
        public override bool IsSatisfiedBy(LoanApplication loanApplication)
        {
            return loanApplication.Loan.LoanAmount < loanApplication.Property.Value;
        }

        public override string Message => "Property value is lower than loan amount";
    }

    public class CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65 : SpecificationBase
    {
        public override bool IsSatisfiedBy(LoanApplication loanApplication)
        {
            var lastInstallmentDate = loanApplication.Loan.LastInstallmentsDate();
            return loanApplication.Customer.AgeInYearsAt(lastInstallmentDate) < 65.Years();
        }

        public override string Message => "Customer age at last installment date is above 65";
    }

    public class InstallmentAmountMustBeLowerThan15PercentOfCustomerIncome : SpecificationBase
    {
        public override bool IsSatisfiedBy(LoanApplication loanApplication)
        {
            return loanApplication.Loan.MonthlyInstallment()
                   < loanApplication.Customer.MonthlyIncome * 15.Percent();
        }

        public override string Message => "Installment is higher than %15 of customer's income.";
    }

    public class CustomerHasNotDebtorRecord : SpecificationBase
    {
        private IDebtorRegistry DebtorRegistry { get; }

        public CustomerHasNotDebtorRecord(IDebtorRegistry debtorRegistry)
        {
            DebtorRegistry = debtorRegistry;
        }
        public override bool IsSatisfiedBy(LoanApplication loanApplication)
        {
            return this.DebtorRegistry.HasDebtor(loanApplication.Customer);
        }

        public override string Message => "Customer has debtor record.";
    }    
    
    
}