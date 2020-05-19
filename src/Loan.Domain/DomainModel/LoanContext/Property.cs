using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Property :ValueObject<Property>
    {
        public Money Value { get; }
        public Address Address { get; }
        
        protected Property()
        {
        }

        public Property(Money value,Address address)
        {
            if (value == null)
                throw new ArgumentException("Value cannot be null");
            if (address==null)
                throw new ArgumentException("Address cannot be null");
            if (value <= Money.Zero)
                throw new ArgumentException("Property value must be higher than 0");
            Value = value;
            Address = address;
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                Value,
                Address
            };
        }
    }
}