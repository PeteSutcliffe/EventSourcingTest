using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class CompetitorRemovedFromFixture : Event
    {
        public Guid Id { get; private set; }
        public Guid FixtureId { get; private set; }

        public CompetitorRemovedFromFixture(Guid id, Guid fixtureId)
        {
            Id = id;
            FixtureId = fixtureId;
        }
    }
}