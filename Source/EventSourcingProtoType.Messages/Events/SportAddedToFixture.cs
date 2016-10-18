using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class SportAddedToFixture : DomainEvent
    {
        public Guid FixtureId { get; private set; }
        public string Title { get; private set; }

        public SportAddedToFixture(Guid aggregateId, Guid fixtureId, string title)
        {
            AggregateId = aggregateId;
            FixtureId = fixtureId;
            Title = title;
        }
    }
}