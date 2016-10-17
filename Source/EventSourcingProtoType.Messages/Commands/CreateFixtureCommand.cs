using System;

namespace EventSourcingProtoType.Messages.Commands
{
    public class CreateFixtureCommand
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public Guid SportId { get; private set; }
        public Guid Competitor1Id { get; private set; }
        public Guid Competitor2Id { get; private set; }

        public CreateFixtureCommand(Guid id, string title, DateTime date, Guid sportId, Guid competitor1Id, Guid competitor2Id)
        {
            Id = id;
            Title = title;
            Date = date;
            SportId = sportId;
            Competitor1Id = competitor1Id;
            Competitor2Id = competitor2Id;
        }
    }
}