using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Scheduler.Entities;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.CommandHandlers
{
    internal class CreateCompetitorCommandHandler : IHandleMessages<CreateCompetitorCommand>
    {
        private readonly IUnitOfWork _uow;

        public CreateCompetitorCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(CreateCompetitorCommand createCompetitorCommand)
        {
            var competitor = new Competitor(createCompetitorCommand.Id, createCompetitorCommand.Name);
            Console.WriteLine($"Created {createCompetitorCommand.Id} : {createCompetitorCommand.Name}");
            _uow.Add(competitor);
            _uow.Commit();
        }
    }
}