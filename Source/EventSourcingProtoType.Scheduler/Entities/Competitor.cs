using System;
using System.Collections.Generic;
using EventSourcingProtoType.Scheduler.Events;

namespace EventSourcingProtoType.Scheduler.Entities
{
    public class Competitor : AggregateRoot
    {
        public override Guid Id { get; protected set; }
        private string _name;
        private readonly Dictionary<Guid, string> _fixtures = new Dictionary<Guid, string>();
         
        public Competitor()
        {
            
        }

        public Competitor(Guid id, string name)
        {
            ApplyChange(new CompetitorCreated(id, name));
        }

        public void ChangeName(string newName)
        {
            if(_name != newName)
                ApplyChange(new CompetitorNameChanged(Id, newName));
        }

        public void AddFixture(Guid fixtureId, string title)
        {
            ApplyChange(new CompetitorAddedToFixture(Id, fixtureId, title));
        }

        public void RemoveFixture(Guid fixtureId)
        {
            ApplyChange(new CompetitorRemovedFromFixture(Id, fixtureId));
        }

        protected void Apply(CompetitorCreated ev)
        {
            Id = ev.Id;
            _name = ev.Name;
        }

        protected void Apply(CompetitorNameChanged ev)
        {
            _name = ev.Name;
        }

        protected void Apply(CompetitorAddedToFixture ev)
        {
            _fixtures.Add(ev.FixtureId, ev.Title);
        }

        protected void Apply(CompetitorRemovedFromFixture ev)
        {
            _fixtures.Remove(ev.FixtureId);
        }
    }
}