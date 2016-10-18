using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class FixtureSportChanged : DomainEvent
    {
        public Guid OldSportId { get; private set; }
        public Guid NewSportId { get; private set; }

        public FixtureSportChanged(Guid aggregateId, Guid oldSportId, Guid newSportId)
        {
            AggregateId = aggregateId;
            OldSportId = oldSportId;
            NewSportId = newSportId;
        }
    }
}