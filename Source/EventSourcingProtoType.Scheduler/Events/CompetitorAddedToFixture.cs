using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class CompetitorAddedToFixture : Event
    {
        public Guid CompetitorId { get; private set; }
        public Guid FixtureId { get; private set; }
        public string Title { get; private set; }

        public CompetitorAddedToFixture(Guid competitorId, Guid fixtureId, string title)
        {
            CompetitorId = competitorId;
            FixtureId = fixtureId;
            Title = title;
        }
    }
}