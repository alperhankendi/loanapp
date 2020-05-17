using System.Collections.Generic;

namespace Loan.Domain
{
    public class ScoringRulesFactory
    {
        private readonly IDebtorRegistry _debtorRegistry;

        public ScoringRulesFactory(IDebtorRegistry debtorRegistry)
        {
            _debtorRegistry = debtorRegistry;
        }
        public ScoringRules DefaultSet => new ScoringRules(new List<SpecificationBase>
        {
            new LoanAmountMustBeLowerThanPropertyValue(),
            new InstallmentAmountMustBeLowerThan15PercentOfCustomerIncome(),
            new CustomerAgeAtTheDateOfLastInstallmentMustBeBelow65(),
            new CustomerHasNotDebtorRecord(_debtorRegistry)
        });
    }
}