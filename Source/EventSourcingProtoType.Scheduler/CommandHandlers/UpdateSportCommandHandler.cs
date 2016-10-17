using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Scheduler.Entities;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.CommandHandlers
{
    internal class UpdateSportCommandHandler : IHandleMessages<UpdateSportCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateSportCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(UpdateSportCommand updateSportCommand)
        {
            var sport = _uow.GetById<Sport>(updateSportCommand.Id);
            sport.ChangeName(updateSportCommand.Name);
            _uow.Commit();
            Console.WriteLine($"Updated {updateSportCommand.Id} : {updateSportCommand.Name}");
        }
    }
}