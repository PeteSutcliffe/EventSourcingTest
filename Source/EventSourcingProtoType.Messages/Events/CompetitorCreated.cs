using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class CompetitorCreated : DomainEvent
    {
        public string Name { get; set; }

        public CompetitorCreated(Guid aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
        }

        public CompetitorCreated()
        {
            
        }
    }
}