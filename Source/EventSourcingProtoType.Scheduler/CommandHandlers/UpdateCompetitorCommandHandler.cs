using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Scheduler.Entities;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.CommandHandlers
{
    internal class UpdateCompetitorCommandHandler : IHandleMessages<UpdateCompetitorCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateCompetitorCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(UpdateCompetitorCommand updateCompetitorCommand)
        {
            var competitor = _uow.GetById<Competitor>(updateCompetitorCommand.Id);
            competitor.ChangeName(updateCompetitorCommand.Name);
            _uow.Commit();

            Console.WriteLine($"Updated {updateCompetitorCommand.Id} : {updateCompetitorCommand.Name}");
        }
    }
}