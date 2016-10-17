using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Scheduler.Dtos;
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
        private readonly DtoRepository _repository;

        public FixtureEventHandler()
        {
            _repository = new DtoRepository();
        }

        public async Task Handle(FixtureCreated message)
        {
            var competitor1 = _repository.Get<CompetitorDto>(message.Competitor1);
            var competitor2 = _repository.Get<CompetitorDto>(message.Competitor2);
            var sport = _repository.Get<SportDto>(message.SportId);

            _repository.Add(new FixtureDto { Id = message.Id, Title = message.Title, Sport = sport, Competitor1 = competitor1, Competitor2 = competitor2});
            Console.WriteLine($"Handled fixture created event {message.Id}:{message.Title}");
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