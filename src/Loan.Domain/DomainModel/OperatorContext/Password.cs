using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Password :ValueObject<Password>
    {
        public string Value { get; }

        protected Password()
        {
        }

        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password cannot be null or empty string");

            Value = value;
        }
        public static Password Of(string password) => new Password(password);
        //override to ToString() 
        public static implicit operator string(Password password) => password.Value;
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}