using System;
using Xunit;

namespace Loan.Domain.Test
{
    public class PropertyTest
    {
        private static readonly Property propOne = new Property(new MonetaryAmount(10_000_000M),
            new Address("Turkey","34840","Istanbul","Cumhuriyet Cad."));
            
        [Fact]
        public void PropertiesWithTheSameValueAndAddress_AreEqual()
        {
            var propTwo = new Property(new MonetaryAmount(10_000_000M),
                new Address("Turkey","34840","Istanbul","Cumhuriyet Cad."));
            
            Assert.True(propOne.Equals(propTwo));
        }
        [Fact]
        public void PropertiesWithTheDiffValueAndSameAddress_AreNotEqual()
        {
            var propTwo = new Property(new MonetaryAmount(10_000_001M),
                new Address("Turkey","34840","Istanbul","Cumhuriyet Cad."));
            
            Assert.False(propOne.Equals(propTwo));
        }
        [Fact]
        public void PropertiesWithTheSameValueAndDiffAddress_AreNotEqual()
        {
            var propTwo = new Property(new MonetaryAmount(10_000_000M),
                new Address("Turkey","34841","Istanbul","Cumhuriyet Cad."));
            
            Assert.False(propOne.Equals(propTwo));
        }

        [Fact]
        public void Property_CannotBeCreatedWithout_Value()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Property(null,
                    new Address("Turkey", "34841", "Istanbul", "Cumhuriyet Cad.")));
            
            Assert.Equal("Value cannot be null",ex.Message); 
        }
        [Fact]
        public void Property_CannotBeCreatedWith_ZeroValue()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Property(MonetaryAmount.Zero, 
                    new Address("Turkey", "34841", "Istanbul", "Cumhuriyet Cad.")));
            Assert.Equal("Property value must be higher than 0",ex.Message);
        }
        [Fact]
        public void Property_CannotBeCreatedWithout_Address()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Property(new MonetaryAmount(10_000_000M),null ));
            
            Assert.Equal("Address cannot be null",ex.Message); 
        }
    }
}