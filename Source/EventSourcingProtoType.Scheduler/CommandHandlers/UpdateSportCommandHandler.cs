using System;
using System.Threading.Tasks;
using EventSourcingProtoType.Messages;
using Rebus.Handlers;

namespace EventSourcingProtoType.Scheduler.CommandHandlers
{
    internal class UpdateSportCommandHandler : IHandleMessages<UpdateSportCommand>
    {
        private readonly IRepository<Sport> _sportRepository;

        public UpdateSportCommandHandler(IRepository<Sport> sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public Task Handle(UpdateSportCommand updateSportCommand)
        {
            Console.WriteLine($"Updated {updateSportCommand.Id} : {updateSportCommand.Name}");
            var sport = _sportRepository.GetById(updateSportCommand.Id);
            sport.ChangeName(updateSportCommand.Name);
            _sportRepository.Save(sport, -1);
            return Task.CompletedTask;
        }
    }
}