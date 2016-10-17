using System;

namespace EventSourcingProtoType.Messages.Commands
{
    public class CreateSportCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public CreateSportCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}