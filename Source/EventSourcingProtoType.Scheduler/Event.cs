using System;

namespace EventSourcingProtoType.Scheduler
{
    public class Event
    {
        public int Version { get; set; }
    }

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