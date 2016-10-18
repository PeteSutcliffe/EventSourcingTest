using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class CompetitorNameChanged : DomainEvent
    {
        public string Name { get; set; }

        public CompetitorNameChanged(Guid aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
        }

        public CompetitorNameChanged()
        {
            
        }
    }
}