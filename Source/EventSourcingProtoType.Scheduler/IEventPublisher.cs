using EventSourcingProtoType.Scheduler.Events;

namespace EventSourcingProtoType.Scheduler
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }

    public class EventPublisher : IEventPublisher
    {
        public void Publish<T>(T @event) where T : Event
        {
            Program.Bus.SendLocal(@event);
        }
    }
}