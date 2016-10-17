using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Scheduler.Entities;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.CommandHandlers
{
    internal class CreateSportCommandHandler : IHandleMessages<CreateSportCommand>
    {
        private readonly IUnitOfWork _uow;

        public CreateSportCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task Handle(CreateSportCommand createSportCommand)
        {
            var sport = new Sport(createSportCommand.Id, createSportCommand.Name);
            _uow.Add(sport);
            _uow.Commit();

            Console.WriteLine($"Created {createSportCommand.Id} : {createSportCommand.Name}");
            return Task.CompletedTask;
        }
    }
}