using System;

namespace EventSourcingProtoType.Web.Models
{
    public class Fixture
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public Sport Sport { get; set; }
        public Competitor Competitor1 { get; set; }
        public Competitor Competitor2 { get; set; }
    }
}