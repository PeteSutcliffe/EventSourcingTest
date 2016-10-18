using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class SportRemovedFromFixture : DomainEvent
    {
        public Guid FixtureId { get; set; }

        public SportRemovedFromFixture(Guid aggregateId, Guid fixtureId)
        {
            AggregateId = aggregateId;
            FixtureId = fixtureId;
        }
    }
}