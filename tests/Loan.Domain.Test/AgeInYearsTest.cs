using System;
using Xunit;

namespace Loan.Domain.Test
{
    public class AgeInYearsTest
    {
        [Fact]
        public void AgeInYears_PersonBorn1980_AfterBirthdateIn2020_40()
        {
            var age = AgeInYears.Between(new DateTime(1980, 7, 20), new DateTime(2021, 3, 14));
            
            Assert.Equal(41.Years(),age);
        }
        [Fact]
        public void AgeInYears_PersonBorn1980_BeforeBirthdateIn2020_40()
        {
            var age = AgeInYears.Between(new DateTime(1980, 7, 20), new DateTime(2021, 3, 14));
            
            Assert.Equal(41.Years(),age);
        }
        [Fact]
        public void AgeInYears_One1980_Two2021_Operators()
        {
            var one = new AgeInYears(60);
            var two = new AgeInYears(41);
            
            Assert.True(one>two);
            Assert.True(one>=two);
            Assert.False(one<two);
            Assert.False(one<=two);
            Assert.False(one==two);
        }
    }
}