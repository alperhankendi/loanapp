namespace Loan.Core
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent message) where TEvent : DomainEvent;
    }
}