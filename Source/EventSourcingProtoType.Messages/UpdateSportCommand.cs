using System;

namespace EventSourcingProtoType.Messages
{
    public class UpdateSportCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public UpdateSportCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}