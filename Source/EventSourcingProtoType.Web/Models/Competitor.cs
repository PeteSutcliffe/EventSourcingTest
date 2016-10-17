using System;

namespace EventSourcingProtoType.Web.Models
{
    public class Competitor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}