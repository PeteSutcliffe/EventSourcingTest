using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class SportAddedToFixture : Event
    {
        public Guid SportId { get; private set; }
        public Guid FixtureId { get; private set; }
        public string Title { get; private set; }

        public SportAddedToFixture(Guid sportId, Guid fixtureId, string title)
        {
            SportId = sportId;
            FixtureId = fixtureId;
            Title = title;
        }
    }
}