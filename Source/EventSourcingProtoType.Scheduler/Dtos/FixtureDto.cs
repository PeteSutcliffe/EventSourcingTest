using System;

namespace EventSourcingProtoType.Scheduler.Dtos
{
    public class FixtureDto : Dto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public SportDto Sport { get; set; }
        public CompetitorDto Competitor1 { get; set; } 
        public CompetitorDto Competitor2 { get; set; } 
    }
}