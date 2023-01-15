using Loan.Core;
using MassTransit;

namespace Loan.Service.Api;

public class EventPublisher : IEventPublisher
{
    private readonly IBusControl serviceBus;

    public EventPublisher(IBusControl serviceBus)
    {
        this.serviceBus = serviceBus;
    }

    public void Publish<TMessage>(TMessage message) where TMessage : DomainEvent
    {
        serviceBus.Publish<TMessage>(message);
    }
}