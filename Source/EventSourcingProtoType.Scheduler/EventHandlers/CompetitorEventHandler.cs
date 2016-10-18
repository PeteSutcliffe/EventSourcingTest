using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages.Events;
using EventSourcingProtoType.Scheduler.Dtos;
using MongoDB.Driver;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.EventHandlers
{
    public class CompetitorEventHandler : IHandleMessages<CompetitorCreated>,
        IHandleMessages<CompetitorNameChanged>,
        IHandleMessages<CompetitorAddedToFixture>,
        IHandleMessages<CompetitorRemovedFromFixture>
    {
        private readonly DtoRepository _repository;

        public CompetitorEventHandler()
        {
            _repository = new DtoRepository();
        }

        public async Task Handle(CompetitorCreated message)
        {
            _repository.Add(new CompetitorDto() { Id = message.AggregateId, Name = message.Name });
            _repository.Add(new CompetitorToFixtureMapDto { Id = message.AggregateId });
            Console.WriteLine($"Handled fixture created event {message.AggregateId}:{message.Name}");
        }

        public async Task Handle(CompetitorNameChanged message)
        {
            var updateBuilder = new UpdateDefinitionBuilder<CompetitorDto>();
            _repository.Update(message.AggregateId, updateBuilder.Set(s => s.Name, message.Name));

            var fixtureMap = _repository.Get<CompetitorToFixtureMapDto>(message.AggregateId);

            var updateFixtureBuilder = new UpdateDefinitionBuilder<FixtureDto>();


            var updateDefinition1 = updateFixtureBuilder.Set(s => s.Competitor1.Name, message.Name);
            var updateDefinition2 = updateFixtureBuilder.Set(s => s.Competitor2.Name, message.Name);

            foreach (var fixtureId in fixtureMap.FixtureIds)
            {
                var fixture = _repository.Get<FixtureDto>(fixtureId);
                if(fixture.Competitor1.Id == message.AggregateId)
                    _repository.Update(fixtureId, updateDefinition1);
                if(fixture.Competitor2.Id == message.AggregateId)
                    _repository.Update(fixtureId, updateDefinition2);
            }

            Console.WriteLine($"Handled competitor name changed event {message.AggregateId}:{message.Name}");
        }

        public async Task Handle(CompetitorAddedToFixture message)
        {
            var fixtureMap = _repository.Get<CompetitorToFixtureMapDto>(message.AggregateId);
            fixtureMap.FixtureIds.Add(message.FixtureId);
            _repository.Upsert(fixtureMap);

            Console.WriteLine("Handled competitor added to fixture event");
        }

        public async Task Handle(CompetitorRemovedFromFixture message)
        {
            var fixtureMap = _repository.Get<CompetitorToFixtureMapDto>(message.AggregateId);
            fixtureMap.FixtureIds.Remove(message.FixtureId);
            _repository.Upsert(fixtureMap);

            Console.WriteLine("Handled competitor removed from fixture event");
        }
    }
}