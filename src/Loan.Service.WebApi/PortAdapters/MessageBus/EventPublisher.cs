using System.Diagnostics.Tracing;
using Loan.Core;
using MassTransit;

namespace Loan.Service.WebApi
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IBus serviceBus;

        public EventPublisher(MassTransit.IBus serviceBus)
        {
            this.serviceBus = serviceBus;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : DomainEvent
        {
            serviceBus.Publish(message);
        }
    }
}