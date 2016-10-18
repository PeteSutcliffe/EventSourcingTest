using System;
using System.Collections.Generic;

namespace EventSourcingProtoType.Scheduler.Dtos
{
    public class CompetitorToFixtureMapDto : Dto
    {
        public List<Guid> FixtureIds { get; set; } = new List<Guid>(); 
    }
}