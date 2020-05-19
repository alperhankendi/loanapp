using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class NationalIdentifier : ValueObject<NationalIdentifier>
    {
        protected NationalIdentifier()
        { }

        public NationalIdentifier(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("National Identifier cannot be null or empty string");
            if(value.Length!=11)
                throw new ArgumentException("National Identifier must be 11 chars long");
            Value = value;
        }
        public string Value { get; }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}