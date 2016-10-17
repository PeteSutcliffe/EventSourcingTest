using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Scheduler.Dtos;
using EventSourcingProtoType.Scheduler.Events;
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
            _repository.Add(new SportDto { Id = sport.Id, Name = sport.Name});
            Console.WriteLine($"Handled created event {sport.Id}:{sport.Name}");
            return Task.CompletedTask;
        }

        public Task Handle(SportNameChanged message)
        {
            var updateBuilder = new UpdateDefinitionBuilder<SportDto>();

            _repository.Update(message.Id, updateBuilder.Set(s => s.Name, message.Name));
            Console.WriteLine($"Handled name changed event {message.Id}:{message.Name}");
            return Task.CompletedTask;
        }

        public async Task Handle(SportAddedToFixture message)
        {
            Console.WriteLine("Handled sport added to fixture event");
        }

        public async Task Handle(SportRemovedFromFixture message)
        {
            Console.WriteLine("Handled sport removed from fixture event");
        }
    }
}