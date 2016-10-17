using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class FixtureTitleChanged : Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public FixtureTitleChanged(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}