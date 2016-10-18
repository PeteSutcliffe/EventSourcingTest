using System;
using EventSourcingProtoType.Messages.Events;

namespace EventSourcingProtoType.Scheduler.Entities
{
    public class Fixture : AggregateRoot
    {
        public override Guid Id { get; protected set; }

        private string _title;
        private DateTime _date;
        private Guid _sportId;
        private Guid _competitor1;
        private Guid _competitor2;

        public Fixture()
        {
            
        }

        public Fixture(Guid id, string title, DateTime date, Guid sportId, Guid competitor1Id, Guid competitor2Id,
            Func<Guid, Sport> getSport,
            Func<Guid, Competitor> getCompetitor)
        {
            var sport = getSport(sportId);
            var competitor1 = getCompetitor(competitor1Id);
            var competitor2 = getCompetitor(competitor2Id);

            sport.AddFixture(id, _title);
            competitor1.AddFixture(id, _title);
            competitor2.AddFixture(id, _title);
            ApplyChange(new FixtureCreated(id, title, date, sportId, competitor1Id, competitor2Id));
        }

        public void Update(string title, 
            DateTime date, 
            Guid sportId, 
            Guid competitor1Id, 
            Guid competitor2Id,
            Func<Guid, Sport> getSport,
            Func<Guid, Competitor> getCompetitor)
        {
            if (title != _title)
            {
                ApplyChange(new FixtureTitleChanged(Id, title));
            }

            if (date != _date)
            {
                ApplyChange(new FixtureDateChanged(Id, date));
            }

            if (sportId != _sportId)
            {
                var oldSport = getSport(_sportId);
                oldSport.RemoveFixture(this.Id);
                var newSport = getSport(sportId);
                newSport.AddFixture(this.Id, _title);
                ApplyChange(new FixtureSportChanged(Id, _sportId, sportId));
            }

            if (competitor1Id != _competitor1)
            {
                var oldCompetitor = getCompetitor(_competitor1);
                oldCompetitor.RemoveFixture(Id);
                var newCompetitor = getCompetitor(competitor1Id);
                newCompetitor.AddFixture(Id, _title);
                ApplyChange(new FixtureCompetitor1Changed(Id, _competitor1, competitor1Id));
            }

            if (competitor2Id != _competitor2)
            {
                ApplyChange(new FixtureCompetitor2Changed(Id, _competitor2, competitor2Id));
            }
        }

        protected void Apply(FixtureCreated ev)
        {
            Id = ev.AggregateId;
            _title = ev.Title;
            _date = ev.Date;
            _sportId = ev.SportId;
            _competitor1 = ev.Competitor1;
            _competitor2 = ev.Competitor2;
        }

        protected void Apply(FixtureTitleChanged ev)
        {
            _title = ev.Title;
        }

        protected void Apply(FixtureDateChanged ev)
        {
            _date = ev.Date;
        }

        protected void Apply(FixtureSportChanged ev)
        {
            _sportId = ev.NewSportId;
        }

        protected void Apply(FixtureCompetitor1Changed ev)
        {
            _competitor1 = ev.NewCompetitorId;
        }

        protected void Apply(FixtureCompetitor2Changed ev)
        {
            _competitor2 = ev.NewCompetitorId;
        }
    }
}