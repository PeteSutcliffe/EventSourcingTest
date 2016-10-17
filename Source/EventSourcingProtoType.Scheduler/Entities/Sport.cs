using System;
using System.Collections.Generic;
using EventSourcingProtoType.Scheduler.Events;

namespace EventSourcingProtoType.Scheduler.Entities
{
    public class Sport : AggregateRoot
    {
        public override Guid Id { get; protected set; }
        private string _name;
        private readonly Dictionary<Guid,string> _fixtures = new Dictionary<Guid, string>();

        public Sport()
        {
            
        }

        public Sport(Guid id, string name)
        {
            ApplyChange(new SportCreated(id, name));
        }

        public void ChangeName(string newName)
        {
            if(_name != newName)
                ApplyChange(new SportNameChanged(Id, newName));
        }

        public void AddFixture(Guid fixtureId, string title)
        {
            ApplyChange(new CompetitorAddedToFixture(Id, fixtureId, title));
        }

        public void RemoveFixture(Guid fixtureId)
        {
            ApplyChange(new SportRemovedFromFixture(Id, fixtureId));
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

        protected void Apply(SportAddedToFixture ev)
        {
            _fixtures.Add(ev.SportId, ev.Title);
        }

        protected void Apply(SportRemovedFromFixture ev)
        {
            _fixtures.Remove(ev.FixtureId);
        }
    }
}