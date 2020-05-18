using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Registration : ValueObject<Registration>
    {
        public DateTime RegistrationDate { get;}
        public OperatorId RegisteredBy { get;}
        protected Registration()
        {
        }
        public Registration(DateTime registrationDate,OperatorId registeredBy)
        {
            RegistrationDate = registrationDate;
            RegisteredBy = registeredBy;
        }
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