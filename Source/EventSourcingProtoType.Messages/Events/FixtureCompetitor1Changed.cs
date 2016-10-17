using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class FixtureCompetitor1Changed : Event
    {
        public Guid Id { get; set; }
        public Guid OldCompetitorId { get; set; }
        public Guid NewCompetitorId { get; set; }

        public FixtureCompetitor1Changed(Guid id, Guid oldCompetitorId, Guid newCompetitorId)
        {
            Id = id;
            OldCompetitorId = oldCompetitorId;
            NewCompetitorId = newCompetitorId;
        }
    }
}