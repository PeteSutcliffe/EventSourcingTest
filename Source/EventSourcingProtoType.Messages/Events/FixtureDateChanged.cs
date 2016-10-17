using System;

namespace EventSourcingProtoType.Scheduler.Events
{
    public class FixtureDateChanged : Event
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public FixtureDateChanged(Guid id, DateTime date)
        {
            Id = id;
            Date = date;
        }
    }
}