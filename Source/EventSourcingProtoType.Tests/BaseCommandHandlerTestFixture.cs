using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingProtoType.Messages.Events;
using EventSourcingProtoType.Scheduler;
using NSubstitute;
using NUnit.Framework;
using Rebus.Handlers;

namespace EventSourcingProtoType.Tests
{
    [TestFixture]
    public abstract class BaseCommandHandlerTestFixture<TCommand, TCommandHandler> where TCommandHandler : IHandleMessages<TCommand>
    {
        protected UnitOfWork Uow;
        protected IEventStore EventStore;

        [SetUp]
        public void Setup()
        {
            RaisedEvents = new List<DomainEvent>();

            EventStore = Substitute.For<IEventStore>();

            var events = Given();

            EventStore.GetEventsForAggregate(Arg.Any<Guid>())
                .Returns(info => events.Where(e => e.AggregateId == (Guid)info[0]).ToList());

            EventStore.When(es => es.SaveEvents(Arg.Any<Guid>(), Arg.Any<IEnumerable<DomainEvent>>(), Arg.Any<int>()))
                .Do(x => RaisedEvents.AddRange(x.Arg<IEnumerable<DomainEvent>>()));

            Uow = new UnitOfWork(new RepositoryFactory(EventStore), EventStore);

            Handler.Handle(Command);
        }

        protected List<DomainEvent> RaisedEvents { get; private set; }

        protected virtual IEnumerable<DomainEvent> Given()
        {
            return new DomainEvent[0];
        }

        protected abstract TCommandHandler Handler { get; }

        protected abstract TCommand Command { get; }
    }

    public class DummyEventStore : IEventStore
    {
        private readonly IEnumerable<DomainEvent> _events;

        public DummyEventStore(IEnumerable<DomainEvent> events)
        {
            _events = events;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion)
        {
            throw new NotImplementedException();
        }

        public List<DomainEvent> GetEventsForAggregate(Guid aggregateId)
        {
            return null;
        }
    }
}
