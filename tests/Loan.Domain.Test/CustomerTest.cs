using System;
using Xunit;

namespace Loan.Domain.Test
{
    public class CustomerTest
    {
        [Fact]
        public void Customer_Born1960_IsAt2031_71YearsOld()
        {
            var customer = new Customer
            (new NationalIdentifier("11704301782"),
                new Name("Alper", "Hankendi"),
                new DateTime(1960, 7, 20),
                new MonetaryAmount(5_000M),
                new Address("Turkey", "34840", "Istanbul", "Cumhuriyet Cd.")
            );
            var ageAt2031 = customer.AgeInYearsAt(new DateTime(2031, 1, 1));
            
            Assert.Equal(71.Years(),ageAt2031);
        }
        [Fact]
        public void Customer_Born1980_IsAt2021_41YearsOld()
        {
            var customer = new Customer
            (new NationalIdentifier("11704301782"),
                new Name("Alper", "Hankendi"),
                new DateTime(1980, 7, 20),
                new MonetaryAmount(5_000M),
                new Address("Turkey", "34840", "Istanbul", "Cumhuriyet Cd.")
            );
            var ageAt2021 = customer.AgeInYearsAt(new DateTime(2021, 1, 1));
            
            Assert.Equal(41.Years(),ageAt2021);
        }
        [Fact]
        public void Customer_CannotBeCreatedWithout_NationalIdentifier()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            
                new Customer
                (   null,
                    new Name("Alper","Hankendi"),
                    new DateTime(1980,7,20),
                    new MonetaryAmount(5_000M),
                    new Address("Turkey","34840","Istanbul","Cumhuriyet Cd.")
                )
            );
            
            Assert.Equal("National identifier cannot be null",ex.Message);
        }
        [Fact]
        public void Customer_CannotBeCreatedWithout_Name()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            
                new Customer
                (   new NationalIdentifier("11704301782"),
                    null,
                    new DateTime(1980,7,20),
                    new MonetaryAmount(5_000M),
                    new Address("Turkey","34840","Istanbul","Cumhuriyet Cd.")
                )
            );
            
            Assert.Equal("Name cannot be null",ex.Message);
        }
        [Fact]
        public void Customer_CannotBeCreatedWithout_Birthdate()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            
                new Customer
                (   new NationalIdentifier("11704301782"),
                    new Name("Alper","Hankendi"),
                    default,
                    new MonetaryAmount(5_000M),
                    new Address("Turkey","34840","Istanbul","Cumhuriyet Cd.")
                )
            );
            
            Assert.Equal("Birthdate cannot be empty",ex.Message);
        }
        
        [Fact]
        public void Customer_CannotBeCreatedWithout_MonthlyIncome()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            
                new Customer
                (   new NationalIdentifier("11704301782"),
                    new Name("Alper","Hankendi"),
                    new DateTime(1980,7,20),
                    null,
                    new Address("Turkey","34840","Istanbul","Cumhuriyet Cd.")
                )
            );
            Assert.Equal("Monthly income cannot be null",ex.Message);
        }
        
        [Fact]
        public void Customer_CannotBeCreatedWithout_Address()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            
                new Customer
                (   new NationalIdentifier("11704301782"),
                    new Name("Alper","Hankendi"),
                    new DateTime(1980,7,20),
                    new MonetaryAmount(5_000M),
                    null
                )
            );
            
            Assert.Equal("Address cannot be null",ex.Message);
        }
    }
}