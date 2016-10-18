using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Messages.Events;
using EventSourcingProtoType.Scheduler.CommandHandlers;
using NUnit.Framework;

namespace EventSourcingProtoType.Tests
{
    public class ChangeSportCommandHandlerTest : BaseCommandHandlerTestFixture<UpdateSportCommand, UpdateSportCommandHandler>
    {
        private readonly Guid _guid = new Guid("BC3267CE-84C5-4B79-BA22-126E517CDF59");

        protected override UpdateSportCommandHandler Handler => new UpdateSportCommandHandler(Uow);

        protected override UpdateSportCommand Command => new UpdateSportCommand(_guid, "Soccer");

        protected override IEnumerable<DomainEvent> Given()
        {
            return new DomainEvent[]
            {
                new SportCreated(_guid, "Football")
            };
        }

        [Test]
        public void SportNameChanged_Event_Is_Raised()
        {
            Assert.That(RaisedEvents.First() is SportNameChanged);
        }

        [Test]
        public void SportNameChanged_Event_Has_Correct_Values()
        {
            var ev = RaisedEvents.First() as SportNameChanged;
            Assert.That(ev.AggregateId, Is.EqualTo(_guid));
            Assert.That(ev.Name, Is.EqualTo("Soccer"));
        }
    }
}