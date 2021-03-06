﻿using System;

namespace EventSourcingProtoType.Messages.Commands
{
    public class UpdateCompetitorCommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public UpdateCompetitorCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}