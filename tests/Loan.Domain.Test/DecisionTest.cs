using Loan.Core;
using Xunit;

namespace Loan.Domain.Test
{
    public class DecisionTest
    {
        [Fact]
        public void TwoDecisionAreSame_ShouldReturnTrue()
        {
            var d = SystemTime.Now();
            var @operator = new OperatorId(System.Guid.NewGuid());
            var dec1 = new Decision(d, @operator);
            var dec2 = new Decision(d, @operator);
            var dec3 = new Decision(SystemTime.Now(), @operator);
            
            Assert.Equal(dec1,dec2);
            Assert.NotEqual(dec1,dec3);
            Assert.NotEqual(dec2,dec3);
        }
    }
}