using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class SportCreated : Event
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public SportCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public SportCreated()
        {
            
        }
    }
}