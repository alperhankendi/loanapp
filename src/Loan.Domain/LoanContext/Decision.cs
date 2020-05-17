using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Decision : ValueObject<Decision>
    {
        public DateTime DecisionDate { get; set; }
        public OperatorId DecisionBy { get; set; }
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