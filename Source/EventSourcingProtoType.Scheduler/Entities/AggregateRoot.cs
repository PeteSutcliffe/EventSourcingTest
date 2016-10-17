using System;
using System.Collections.Generic;
using System.Reflection;
using EventSourcingProtoType.Scheduler.Events;

namespace EventSourcingProtoType.Scheduler.Entities
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public abstract Guid Id { get; protected set; }
        public int Version { get; internal set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            var method = GetType().GetMethod("Apply", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {@event.GetType()}, null);
            if(method == null) throw new InvalidOperationException("Apply method not found");
            method.Invoke(this, new[] {@event});
            if (isNew) _changes.Add(@event);
        }
    }
}