using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler
{
    internal class CreateSportCommandHandler : IHandleMessages<CreateSportCommand>
    {
        private readonly IRepository<Sport> _sportRepository;

        public CreateSportCommandHandler(IRepository<Sport> sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public Task Handle(CreateSportCommand createSportCommand)
        {
            var sport = new Sport(createSportCommand.Id, createSportCommand.Name);
            Console.WriteLine($"Created {createSportCommand.Id} : {createSportCommand.Name}");
            _sportRepository.Save(sport, -1);
            return Task.CompletedTask;
        }
    }
}