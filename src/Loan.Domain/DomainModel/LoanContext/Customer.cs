using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Customer : ValueObject<Customer>
    {
        public Name Name { get; set; }
        public DateTime Birthdate { get; }
        public Money MonthlyIncome { get; }
        public Address Address { get; }
        
        public NationalIdentifier NationalIdentifier { get; }

        protected Customer()
        {
        }

        public Customer(NationalIdentifier nationalIdentifier,
            Name name,DateTime birthdate,
            Money monthlyIncome,
            Address address)
        {
            if (nationalIdentifier == null)
            {
                throw new ArgumentException("National identifier cannot be null");
            }
            if (name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }
            if (monthlyIncome == null)
            {
                throw new ArgumentException("Monthly income cannot be null");
            }
            if (address == null)
            {
                throw new ArgumentException("Address cannot be null");
            }
            if (birthdate == default)
            {
                throw new ArgumentException("Birthdate cannot be empty");
            }
            NationalIdentifier = nationalIdentifier;
            Birthdate = birthdate;
            MonthlyIncome = monthlyIncome;
            Address = address;
            Name = name;
        }        
        public AgeInYears AgeInYearsAt(DateTime date)
        {
            return AgeInYears.Between(Birthdate, date);
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<object>
            {
                NationalIdentifier,
                Name,
                Birthdate,
                MonthlyIncome,
                Address
            };
        }
    }
}