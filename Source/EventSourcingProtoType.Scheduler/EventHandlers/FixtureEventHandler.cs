using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Scheduler.Events;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.EventHandlers
{
    public class FixtureEventHandler : IHandleMessages<FixtureCreated>,
        IHandleMessages<FixtureCompetitor1Changed>,
        IHandleMessages<FixtureCompetitor2Changed>,
        IHandleMessages<FixtureDateChanged>,
        IHandleMessages<FixtureSportChanged>,
        IHandleMessages<FixtureTitleChanged>
    {
        public async Task Handle(FixtureCreated message)
        {
            Console.WriteLine("Handled fixture created event");
        }

        public async Task Handle(FixtureCompetitor1Changed message)
        {
            Console.WriteLine("Handled fixture competitor 1 changed event");
        }

        public async Task Handle(FixtureCompetitor2Changed message)
        {
            Console.WriteLine("Handled fixture competitor 2 changed event");
        }

        public async Task Handle(FixtureDateChanged message)
        {
            Console.WriteLine("Handled fixture date changed event");
        }

        public async Task Handle(FixtureSportChanged message)
        {
            Console.WriteLine("Handled fixture sport changed event");
        }

        public async Task Handle(FixtureTitleChanged message)
        {
            Console.WriteLine("Handled fixture title changed event");
        }
    }
}