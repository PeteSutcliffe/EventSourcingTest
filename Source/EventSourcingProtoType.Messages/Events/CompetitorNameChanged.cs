using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class CompetitorNameChanged : Event
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public CompetitorNameChanged(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public CompetitorNameChanged()
        {
            
        }
    }
}