using System;

namespace Loan.Core
{
    public class DomainEvent
    {
        public Guid Id { get; protected set; }
        public DateTime OccuredOn { get;protected set; }
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = SystemTime.Now();
        }
    }
}