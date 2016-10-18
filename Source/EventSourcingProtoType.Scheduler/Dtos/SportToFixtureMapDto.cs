using System;
using System.Collections.Generic;

namespace EventSourcingProtoType.Scheduler.Dtos
{
    public class SportToFixtureMapDto : Dto
    {
        public List<Guid> FixtureIds { get; set; } = new List<Guid>(); 
    }
}