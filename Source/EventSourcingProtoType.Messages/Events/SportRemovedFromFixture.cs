using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class SportRemovedFromFixture : Event
    {
        public Guid Id { get; set; }
        public Guid FixtureId { get; set; }

        public SportRemovedFromFixture(Guid id, Guid fixtureId)
        {
            Id = id;
            FixtureId = fixtureId;
        }
    }
}