using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class FixtureCreated : Event
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public Guid SportId { get; private set; }
        public Guid Competitor1 { get; private set; }
        public Guid Competitor2 { get; private set; }

        public FixtureCreated(Guid id, string title, DateTime date, Guid sportId, Guid competitor1, Guid competitor2)
        {
            Id = id;
            Title = title;
            Date = date;
            SportId = sportId;
            Competitor1 = competitor1;
            Competitor2 = competitor2;
        }
    }
}