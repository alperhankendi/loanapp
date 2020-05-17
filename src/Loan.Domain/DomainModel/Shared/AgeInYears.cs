using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class AgeInYears : ValueObject<AgeInYears> , IComparable<AgeInYears>
    {
        private readonly int age;

        public AgeInYears(int age)
        {
            this.age = age;
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return age;
        }

        public static AgeInYears Between(DateTime start, DateTime end)
        {
            return new AgeInYears(end.Year-start.Year);
        }

        public static bool operator >(AgeInYears one, AgeInYears two) => one.CompareTo(two) > 0;
        public static bool operator <(AgeInYears one, AgeInYears two) => one.CompareTo(two) < 0;
        public static bool operator >=(AgeInYears one, AgeInYears two) => one.CompareTo(two) >= 0;
        public static bool operator <=(AgeInYears one, AgeInYears two) => one.CompareTo(two) <= 0;
        public int CompareTo(AgeInYears other)
        {
            return this.age.CompareTo(other.age);
        }
    }

    public static class AgeInYearsExtentions
    {
        public static AgeInYears Years(this int age) => new AgeInYears(age);
    }
}