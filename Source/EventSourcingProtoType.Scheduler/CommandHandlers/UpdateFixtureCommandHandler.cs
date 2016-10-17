using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Messages.Commands;
using EventSourcingProtoType.Scheduler.Entities;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.CommandHandlers
{
    internal class UpdateFixtureCommandHandler : IHandleMessages<UpdateFixtureCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateFixtureCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(UpdateFixtureCommand command)
        {
            var fixture = _uow.GetById<Fixture>(command.Id);
            fixture.Update(command.Title, 
                command.Date, 
                command.SportId, 
                command.Competitor1Id, 
                command.Competitor2Id,
                id => _uow.GetById<Sport>(id),
                id => _uow.GetById<Competitor>(id));

            _uow.Commit();

            Console.WriteLine($"Updated {command.Id} : {command.Title}");
        }
    }
}