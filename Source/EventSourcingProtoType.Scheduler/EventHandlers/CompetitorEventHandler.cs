using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Scheduler.Events;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.EventHandlers
{
    public class CompetitorEventHandler : IHandleMessages<CompetitorCreated>,
        IHandleMessages<CompetitorNameChanged>,
        IHandleMessages<CompetitorAddedToFixture>,
        IHandleMessages<CompetitorRemovedFromFixture>
    {
        public async Task Handle(CompetitorCreated message)
        {
            Console.WriteLine("Handled competitor created event");
        }

        public async Task Handle(CompetitorNameChanged message)
        {
            Console.WriteLine("Handled competitor changed event");
        }

        public async Task Handle(CompetitorAddedToFixture message)
        {
            Console.WriteLine("Handled competitor added to fixture event");
        }

        public async Task Handle(CompetitorRemovedFromFixture message)
        {
            Console.WriteLine("Handled competitor removed from fixture event");
        }
    }
}