using System;

namespace Loan.Core
{
    public class DomainEvent
    {
        public Guid Id { get; protected set; }
        public DateTime OccuredOn { get;protected set; }

        protected DomainEvent(Guid id,DateTime occuredOn)
        {
            Id = id;
            OccuredOn = occuredOn;
        }
        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = SystemTime.Now();
        }
    }
}