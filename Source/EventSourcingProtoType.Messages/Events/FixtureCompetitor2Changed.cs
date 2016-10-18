using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class FixtureCompetitor2Changed : DomainEvent
    {
        public Guid OldCompetitorId { get; set; }
        public Guid NewCompetitorId { get; set; }

        public FixtureCompetitor2Changed(Guid aggregateId, Guid oldCompetitorId, Guid newCompetitorId)
        {
            AggregateId = aggregateId;
            OldCompetitorId = oldCompetitorId;
            NewCompetitorId = newCompetitorId;
        }
    }
}