using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class FixtureSportChanged : Event
    {
        public Guid Id { get; private set; }
        public Guid OldSportId { get; private set; }
        public Guid NewSportId { get; private set; }

        public FixtureSportChanged(Guid id, Guid oldSportId, Guid newSportId)
        {
            Id = id;
            OldSportId = oldSportId;
            NewSportId = newSportId;
        }
    }
}