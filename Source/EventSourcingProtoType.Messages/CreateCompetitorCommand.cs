using System;

namespace EventSourcingProtoType.Messages
{
    public class CreateCompetitorCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public CreateCompetitorCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}