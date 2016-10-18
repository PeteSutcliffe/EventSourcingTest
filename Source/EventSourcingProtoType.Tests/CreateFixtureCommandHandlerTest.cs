using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Messages.Events;
using EventSourcingProtoType.Scheduler.CommandHandlers;
using NUnit.Framework;

namespace EventSourcingProtoType.Tests
{
    public class CreateFixtureCommandHandlerTest : BaseCommandHandlerTestFixture<CreateFixtureCommand, CreateFixtureCommandHandler>
    {
        private readonly Guid _guid = new Guid("BC3267CE-84C5-4B79-BA22-126E517CDF59");
        private readonly Guid _sportId = new Guid("04CF491C-7A89-4555-BFED-5FE3607915F6");
        private readonly Guid _competitor1Id = new Guid("CD9EC7FF-6744-45B0-BE4E-0EA954FDB461");
        private readonly Guid _competitor2Id = new Guid("116C0467-EDC4-4A7D-AF46-EAAF3D79B9FD");

        protected override CreateFixtureCommandHandler Handler => new CreateFixtureCommandHandler(Uow);

        protected override CreateFixtureCommand Command => 
            new CreateFixtureCommand(_guid, 
                "Fa cup final", 
                DateTime.Today, 
                _sportId, 
                _competitor1Id, 
                _competitor2Id);

        protected override IEnumerable<DomainEvent> Given()
        {
            return new DomainEvent[]
            {
                new SportCreated(_sportId, "Football"),
                new CompetitorCreated(_competitor1Id, "Team 1"), 
                new CompetitorCreated(_competitor2Id, "Team 2"), 
            };
        }

        [Test]
        public void EventsAreRaised()
        {
            Assert.That(RaisedEvents.Any(e => e is FixtureCreated));
            Assert.That(RaisedEvents.Any(e => e is SportAddedToFixture));
            Assert.That(RaisedEvents.Count(e => e is CompetitorAddedToFixture), Is.EqualTo(2));
        }

        [Test]
        public void FixtureCreated_Event_Has_Correct_Values()
        {
            var ev = RaisedEvents.Single(e => e is FixtureCreated) as FixtureCreated;
            Assert.That(ev.AggregateId, Is.EqualTo(_guid));
            Assert.That(ev.Title, Is.EqualTo("Fa cup final"));
            Assert.That(ev.Date, Is.EqualTo(DateTime.Today));
            Assert.That(ev.SportId, Is.EqualTo(_sportId));
            Assert.That(ev.Competitor1, Is.EqualTo(_competitor1Id));
            Assert.That(ev.Competitor2, Is.EqualTo(_competitor2Id));
        }
    }
}