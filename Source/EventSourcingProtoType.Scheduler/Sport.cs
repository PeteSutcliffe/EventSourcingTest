using System;

namespace EventSourcingProtoType.Scheduler
{
    public class Sport : AggregateRoot
    {
        public override Guid Id { get; protected set; }
        private string _name;

        public Sport()
        {
            
        }

        public Sport(Guid id, string name)
        {
            ApplyChange(new SportCreated(id, name));
        }

        public void ChangeName(string newName)
        {
            ApplyChange(new SportNameChanged(Id, newName));
        }

        protected void Apply(SportCreated ev)
        {
            Id = ev.Id;
            _name = ev.Name;
        }

        protected void Apply(SportNameChanged ev)
        {
            _name = ev.Name;
        }
    }
}