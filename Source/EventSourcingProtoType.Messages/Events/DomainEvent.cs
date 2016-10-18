using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class DomainEvent
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }
}