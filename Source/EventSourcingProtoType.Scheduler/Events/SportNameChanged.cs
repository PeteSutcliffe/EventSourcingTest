using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class SportNameChanged : Event
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public SportNameChanged(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public SportNameChanged()
        {
            
        }
    }
}