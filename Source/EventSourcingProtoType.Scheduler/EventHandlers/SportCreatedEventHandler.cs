using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages.Events;
using EventSourcingProtoType.Scheduler.Dtos;
using MongoDB.Driver;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.EventHandlers
{
    public class SportCreatedEventHandler : IHandleMessages<SportCreated>, 
        IHandleMessages<SportNameChanged>,
        IHandleMessages<SportAddedToFixture>,
        IHandleMessages<SportRemovedFromFixture>
    {
        private readonly DtoRepository _repository;

        public SportCreatedEventHandler()
        {
            _repository = new DtoRepository();
        }

        public Task Handle(SportCreated sport)
        {
            _repository.Add(new SportDto { Id = sport.AggregateId, Name = sport.Name});
            _repository.Add(new SportToFixtureMapDto { Id = sport.AggregateId });

            Console.WriteLine($"Handled sport created event {sport.AggregateId}:{sport.Name}");
            return Task.CompletedTask;
        }

        public Task Handle(SportNameChanged message)
        {
            var updateBuilder = new UpdateDefinitionBuilder<SportDto>();
            _repository.Update(message.AggregateId, updateBuilder.Set(s => s.Name, message.Name));

            var fixtureMap = _repository.Get<SportToFixtureMapDto>(message.AggregateId);

            var updateFixtureBuilder = new UpdateDefinitionBuilder<FixtureDto>();
            var updateDefinition = updateFixtureBuilder.Set(s => s.Sport.Name, message.Name);

            foreach (var fixtureId in fixtureMap.FixtureIds)
            {
                _repository.Update(fixtureId, updateDefinition);
            }

            Console.WriteLine($"Handled sport name changed event {message.AggregateId}:{message.Name}");
            return Task.CompletedTask;
        }

        public async Task Handle(SportAddedToFixture message)
        {
            var fixtureMap = _repository.Get<SportToFixtureMapDto>(message.AggregateId);
            fixtureMap.FixtureIds.Add(message.FixtureId);
            _repository.Upsert(fixtureMap);
            Console.WriteLine("Handled sport added to fixture event");
        }

        public async Task Handle(SportRemovedFromFixture message)
        {
            var fixtureMap = _repository.Get<SportToFixtureMapDto>(message.AggregateId);
            fixtureMap.FixtureIds.Remove(message.FixtureId);
            _repository.Upsert(fixtureMap);
            Console.WriteLine("Handled sport removed from fixture event");
        }
    }
}