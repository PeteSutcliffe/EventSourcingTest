using System;
using System.Collections.Generic;
using System.Reflection;
using EventSourcingProtoType.Messages.Events;

namespace EventSourcingProtoType.Scheduler.Entities
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();

        public abstract Guid Id { get; protected set; }
        public int Version { get; internal set; }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<DomainEvent> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(DomainEvent domainEvent)
        {
            ApplyChange(domainEvent, true);
        }

        private void ApplyChange(DomainEvent domainEvent, bool isNew)
        {
            var method = GetType().GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {domainEvent.GetType()}, null);
            if(method == null) throw new InvalidOperationException("Apply method not found");
            method.Invoke(this, new[] {domainEvent});
            if (isNew) _changes.Add(domainEvent);
        }
    }
}