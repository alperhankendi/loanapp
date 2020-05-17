using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Registration : ValueObject<Registration>
    {
        public DateTime RegistrationDate { get; set; }
        public string RegisteredBy { get; set; }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                RegisteredBy,
                RegistrationDate
            };
        }
    }
}