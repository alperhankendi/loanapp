using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Name : ValueObject<Name>
    {
        protected Name()
        {
        }
        public Name(string first,string last)
        {
            if (string.IsNullOrWhiteSpace(first))
                throw new ArgumentException("First name cannot be empty");
            if (string.IsNullOrWhiteSpace(last))
                throw new ArgumentException("First name cannot be empty");
            
            First = first;
            Last = last;
        }

        public string First { get; }
        public string Last { get; }

        public override string ToString()
        {
            return $"{First} {Last}";  
        } 
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return First;
            yield return Last;
        }
    }
}