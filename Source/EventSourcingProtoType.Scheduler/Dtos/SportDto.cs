using System;

namespace EventSourcingProtoType.Scheduler.Dtos
{
    public class SportDto : Dto
    {
        public string Name { get; set; } 
    }

    public abstract class Dto
    {
        public Guid Id { get; set; }
    }
}