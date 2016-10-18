using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class SportNameChanged : DomainEvent
    {
        public string Name { get; set; }

        public SportNameChanged(Guid aggregateId, string name)
        {
            AggregateId = aggregateId;
            Name = name;
        }

        public SportNameChanged()
        {
            
        }
    }
}