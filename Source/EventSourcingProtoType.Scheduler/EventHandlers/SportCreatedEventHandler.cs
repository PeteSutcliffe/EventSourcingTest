using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Scheduler.Dtos;
using EventSourcingProtoType.Scheduler.Events;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.EventHandlers
{
    public class SportCreatedEventHandler : IHandleMessages<SportCreated>
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
    }
}