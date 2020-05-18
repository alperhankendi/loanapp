using System;

namespace Loan.Core
{
    public class SystemTime
    {
        private static Func<DateTime> CurrentTimeProvider { get; set; } = () => DateTime.Now;
        
        public static DateTime Now() => CurrentTimeProvider();
    }
}