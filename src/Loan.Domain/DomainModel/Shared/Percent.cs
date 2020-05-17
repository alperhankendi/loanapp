using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class Percent : ValueObject<Percent>, IComparable<Percent>
    {
        public decimal Value { get; }
        public static readonly Percent Zero = new Percent(0M);
        protected Percent()
        {
        }
        public Percent(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Percent value cannot be negative");
            }
            this.Value = value;
        }
        public static bool operator >(Percent one, Percent two) => one.CompareTo(two) > 0;
        public static bool operator <(Percent one, Percent two) => one.CompareTo(two) < 0;
        public static bool operator <=(Percent one, Percent two) => one.CompareTo(two) <= 0;
        public static bool operator >=(Percent one, Percent two) => one.CompareTo(two) >= 0;
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }

        public int CompareTo(Percent other)
        {
            return Value.CompareTo(other.Value);
        }
    }

    public static class PercentExtensions
    {
        public static Percent Percent(this int value) => new Percent(value);
        public static Percent Percent(this decimal value) => new Percent(value);
    }
}