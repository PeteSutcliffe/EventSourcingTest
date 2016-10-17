using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class CompetitorCreated : Event
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public CompetitorCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public CompetitorCreated()
        {
            
        }
    }
}