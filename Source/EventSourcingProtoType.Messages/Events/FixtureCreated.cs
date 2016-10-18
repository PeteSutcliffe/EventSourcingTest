using System;

namespace EventSourcingProtoType.Messages.Events
{
    public class FixtureCreated : DomainEvent
    {
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public Guid SportId { get; private set; }
        public Guid Competitor1 { get; private set; }
        public Guid Competitor2 { get; private set; }

        public FixtureCreated(Guid aggregateId, string title, DateTime date, Guid sportId, Guid competitor1, Guid competitor2)
        {
            AggregateId = aggregateId;
            Title = title;
            Date = date;
            SportId = sportId;
            Competitor1 = competitor1;
            Competitor2 = competitor2;
        }
    }
}