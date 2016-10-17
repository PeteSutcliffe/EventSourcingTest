using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages;
using EventSourcingProtoType.Scheduler.Entities;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.CommandHandlers
{
    internal class CreateFixtureCommandHandler : IHandleMessages<CreateFixtureCommand>
    {
        private readonly IUnitOfWork _uow;

        public CreateFixtureCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(CreateFixtureCommand command)
        {
            var fixture = new Fixture(command.Id, 
                command.Title, 
                command.Date, 
                command.SportId, 
                command.Competitor1Id, 
                command.Competitor2Id,
                sportId => _uow.GetById<Sport>(sportId),
                competitorId => _uow.GetById<Competitor>(competitorId));
            _uow.Add(fixture);
            _uow.Commit();

            Console.WriteLine($"Created {command.Id} : {command.Title}");
        }
    }
}