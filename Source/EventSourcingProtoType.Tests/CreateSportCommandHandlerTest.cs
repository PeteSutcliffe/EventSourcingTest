using System;
using System.Linq;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Messages.Events;
using EventSourcingProtoType.Scheduler.CommandHandlers;
using NUnit.Framework;

namespace EventSourcingProtoType.Tests
{
    public class CreateSportCommandHandlerTest : BaseCommandHandlerTestFixture<CreateSportCommand, CreateSportCommandHandler>
    {
        private Guid _guid = new Guid("BC3267CE-84C5-4B79-BA22-126E517CDF59");

        protected override CreateSportCommandHandler Handler => new CreateSportCommandHandler(Uow);

        protected override CreateSportCommand Command => new CreateSportCommand(_guid, "Tennis");

        [Test]
        public void SportCreated_Event_Is_Raised()
        {
            Assert.That(RaisedEvents.First() is SportCreated);
        }

        [Test]
        public void SportCreated_Event_Has_Correct_Values()
        {
            var ev = RaisedEvents.First() as SportCreated;
            Assert.That(ev.AggregateId, Is.EqualTo(_guid));
            Assert.That(ev.Name, Is.EqualTo("Tennis"));
        }
    }
}