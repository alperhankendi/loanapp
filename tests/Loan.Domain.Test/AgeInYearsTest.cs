using System;
using Xunit;

namespace Loan.Domain.Test
{
    public class DecisionTest
    {
        [Fact]
        public void TwoDecisionAreSame_ShouldReturnTrue()
        {
            var d = DateTime.Now;
            var @operator = new OperatorId(System.Guid.NewGuid());
            var dec1 = new Decision(d, @operator);
            var dec2 = new Decision(d, @operator);
            var dec3 = new Decision(DateTime.Now, @operator);
            
            Assert.Equal(dec1,dec2);
            Assert.NotEqual(dec1,dec3);
            Assert.NotEqual(dec2,dec3);
        }
    }
    
    public class AgeInYearsTest
    {
        [Fact]
        public void AgeInYears_PersonBorn1980_AfterBirthdateIn2020_40()
        {
            var age = AgeInYears.Between(new DateTime(1980, 7, 20), new DateTime(2020, 8, 1));
            
            Assert.Equal(40.Years(),age);
        }
        [Fact]
        public void AgeInYears_PersonBorn1980_BeforeBirthdateIn2020_40()
        {
            var age = AgeInYears.Between(new DateTime(1980, 7, 20), new DateTime(2020, 5, 1));
            
            Assert.Equal(40.Years(),age);
        }
        [Fact]
        public void AgeInYears_One1980_Two2020_Operators()
        {
            var one = new AgeInYears(60);
            var two = new AgeInYears(40);
            
            Assert.True(one>two);
            Assert.True(one>=two);
            Assert.False(one<two);
            Assert.False(one<=two);
            Assert.False(one==two);
        }
    }
}