using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class CompetitorRemovedFromFixture : DomainEvent
    {
        public Guid FixtureId { get; private set; }

        public CompetitorRemovedFromFixture(Guid aggregateId, Guid fixtureId)
        {
            AggregateId = aggregateId;
            FixtureId = fixtureId;
        }
    }
}