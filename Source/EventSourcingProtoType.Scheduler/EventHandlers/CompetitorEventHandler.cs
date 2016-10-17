using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages.Events;
using EventSourcingProtoType.Scheduler.Dtos;
using EventSourcingProtoType.Scheduler.Events;
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
            _repository.Add(new CompetitorDto() { Id = message.Id, Name = message.Name });
            Console.WriteLine($"Handled fixture created event {message.Id}:{message.Name}");
        }

        public async Task Handle(CompetitorNameChanged message)
        {
            var updateBuilder = new UpdateDefinitionBuilder<CompetitorDto>();

            _repository.Update(message.Id, updateBuilder.Set(s => s.Name, message.Name));
            Console.WriteLine($"Handled competitor name changed event {message.Id}:{message.Name}");
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