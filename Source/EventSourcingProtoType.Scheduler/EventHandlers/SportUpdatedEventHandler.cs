using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Scheduler.Dtos;
using EventSourcingProtoType.Scheduler.Events;
using MongoDB.Driver;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.EventHandlers
{
    public class SportUpdatedEventHandler : IHandleMessages<SportNameChanged>
    {
        private readonly DtoRepository _repository;

        public SportUpdatedEventHandler()
        {
            _repository = new DtoRepository();
        }

        public Task Handle(SportNameChanged message)
        {
            var updateBuilder = new UpdateDefinitionBuilder<SportDto>();

            _repository.Update(message.Id, updateBuilder.Set(s => s.Name, message.Name));
            Console.WriteLine($"Handled name changed event {message.Id}:{message.Name}");
            return Task.CompletedTask;
        }
    }
}