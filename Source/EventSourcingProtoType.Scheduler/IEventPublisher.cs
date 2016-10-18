using EventSourcingProtoType.Messages.Events;

namespace EventSourcingProtoType.Scheduler
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : DomainEvent;
    }

    public class EventPublisher : IEventPublisher
    {
        public void Publish<T>(T @event) where T : DomainEvent
        {
            Program.Bus.SendLocal(@event);
        }
    }
}