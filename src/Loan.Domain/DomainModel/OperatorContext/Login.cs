using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Login :ValueObject<Login>
    {
        public string Value { get; }

        protected Login()
        {
        }

        public Login(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Login cannot be null or empty string");

            Value = value;
        }
        public static Login Of(string login) => new Login(login);
        //override to ToString() 
        public static implicit operator string(Login login) => login.Value;
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}