using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class FixtureDateChanged : DomainEvent
    {
        public DateTime Date { get; set; }

        public FixtureDateChanged(Guid aggregateId, DateTime date)
        {
            AggregateId = aggregateId;
            Date = date;
        }
    }
}