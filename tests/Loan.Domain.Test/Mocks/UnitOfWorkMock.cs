using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Loan.Core;

namespace Loan.Domain.Test
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public void CommitChanges()
        {
            
        }
    }

    public class EventPublisherMock : IEventPublisher
    {
        private readonly List<DomainEvent> events = new List<DomainEvent>();
        public void Publish<TEvent>(TEvent message) where TEvent : DomainEvent
        {
            events.Add(message);
        }

        public ReadOnlyCollection<DomainEvent> Events => events.AsReadOnly();
    }
        
}