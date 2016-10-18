using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class CompetitorAddedToFixture : DomainEvent
    {
        public Guid FixtureId { get; private set; }
        public string Title { get; private set; }

        public CompetitorAddedToFixture(Guid competitorId, Guid fixtureId, string title)
        {
            AggregateId = competitorId;
            FixtureId = fixtureId;
            Title = title;
        }
    }
}