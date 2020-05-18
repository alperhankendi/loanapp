using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Decision : ValueObject<Decision>
    {
        public DateTime DecisionDate { get; }
        public OperatorId DecisionBy { get; }

        protected Decision()
        {
            
        }
        public Decision(DateTime decisionDate, OperatorId decisionBy)
        {
            DecisionDate = decisionDate;
            DecisionBy = decisionBy;
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                DecisionBy,
                DecisionDate
            };
        }
    }
}