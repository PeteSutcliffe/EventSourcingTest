using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class SportCreated : DomainEvent
    {
        public string Name { get; set; }

        public SportCreated(Guid aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
        }

        public SportCreated()
        {
            
        }
    }
}