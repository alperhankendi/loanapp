using Loan.Core;

namespace Loan.Domain.Test.Builders
{
    internal class CustomerBuilder
    {
        private Name name = new Name("Alper","Hankendi");
        private NationalIdentifier nationalIdentifier = new NationalIdentifier("11111111111");
        private int age;
        private Money income = new Money(20_000M);
        private Address address = new Address("Turkey","34840","İstanbul","Cumhuriyet Cad.");
        private Email email = new Email("ahankendi@gmail.com");
        public CustomerBuilder WithIdentifier(string nationalId)
        {
            nationalIdentifier = new NationalIdentifier(nationalId);
            return this;
        }
        
        public CustomerBuilder WithName(string first, string last)
        {
            name = new Name(first,last);
            return this;
        }
        
        public CustomerBuilder WithAge(int age)
        {
            this.age = age;
            return this;
        }
        public CustomerBuilder WithEmail(string email)
        {
            this.email = new Email(email);
            return this;
        }
        public CustomerBuilder WithIncome(decimal income)
        {
            this.income = new Money(income);
            return this;
        }
        
        public CustomerBuilder WithAddress(string country,string zip,string city,string street)
        {
            this.address = new Address(country,zip,city,street);
            return this;
        }

        public Customer Build()
        {
            return new Customer
            (
                nationalIdentifier,
                name,
                SystemTime.Now().AddYears(-1*age),
                income,
                address,
                email
            );
        }
        
    }
}