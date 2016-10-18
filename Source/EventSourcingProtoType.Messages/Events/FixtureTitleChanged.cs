using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class FixtureTitleChanged : DomainEvent
    {
        public string Title { get; set; }

        public FixtureTitleChanged(Guid aggregateId, string title)
        {
            AggregateId = aggregateId;
            Title = title;
        }
    }
}