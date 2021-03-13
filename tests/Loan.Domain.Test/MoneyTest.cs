using FluentAssertions;
using Xunit;

namespace Loan.Domain.Test
{
    public class MoneyTest
    {
        [Fact]
        public void The_Same_Amount_are_equal()
        {
            var one = new Money(100M);
            var two = new Money(100M);
            one.Equals(two).Should().BeTrue();
        }
        [Fact]
        public void The_Same_Amount_are_not_equal()
        {
            var one = new Money(100M);
            var two = new Money(110M);
            one.Equals(two).Should().BeFalse();
        }
        [Fact]
        public void not_the_same_amount_are_not_equal()
        {
            var one = new Money(100M);
            var two = new Money(110M);
            (one != two).Should().BeTrue();
        }

        [Fact]
        public void two_amounts_can_be_compared()
        {
            var one = new Money(100M);
            var two = new Money(110M);
            (one < two).Should().BeTrue();
            (one > two).Should().BeFalse();
        }
        [Fact]
        public void can_add_and_sub()
        {
            var one = new Money(100M);
            var two = new Money(110M);
            one.Add(two).Should().Be(new Money(210M));
            two.Subtract(one).Should().Be(new Money(10M));
        }

        [Fact]
        public void can_multiply_by_percent()
        {
            var one = new Money(100M);
            var p = 10.Percent();
            one.MultiplyByPercent(p).Should().Be(new Money(10M));
        }
    }
}