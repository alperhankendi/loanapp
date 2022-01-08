using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Loan.Core;

namespace Loan.Domain
{
    public class Email : ValueObject<Email>
    {
        public string MailValue { get; }

        protected Email()
        {
            
        }
        public Email(string email)
        {
            if (!email.Contains("@")) throw new Exception("Email is invalid");

            MailValue = email;
        }

        public override string ToString()
        {
            return MailValue;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return MailValue;
        }
    }
    public class Address : ValueObject<Address>
    {
        public string Country { get; }
        public string ZipCode { get; }
        public string City { get; }
        public string Street { get; }

        public Address(string country, string zipCode, string city, string street)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty.");
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("Zip code cannot be empty.");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.");
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.");
            if (!new Regex("[0-9]{5}").Match(zipCode).Success)
                throw new ArgumentException("Zip code must be NNNNN format.");
            
            Country = country;
            ZipCode = zipCode;
            City = city;
            Street = street;
        }
        protected Address()
        {
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Country;
            yield return ZipCode;
            yield return City;
            yield return Street;
        }
    }
}