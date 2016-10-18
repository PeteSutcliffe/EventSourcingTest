using System;
using System.Collections.Generic;
using EventSourcingProtoType.Scheduler.Entities;

namespace EventSourcingProtoType.Scheduler
{
    public interface IUnitOfWork
    {
        T GetById<T>(Guid id) where T : AggregateRoot, new();
        void Add(AggregateRoot obj);
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryFactory _factory;
        private readonly IEventStore _eventStore;
        private readonly List<AggregateRoot> _tracking = new List<AggregateRoot>();

        public UnitOfWork(IRepositoryFactory factory, IEventStore eventStore)
        {
            _factory = factory;
            _eventStore = eventStore;
        }

        public T GetById<T>(Guid id) where T : AggregateRoot, new()
        {
            var obj = _factory.Create<T>().GetById(id);
            RegisterForTracking(obj);
            return obj;
        }

        private void RegisterForTracking(AggregateRoot obj)
        {
            _tracking.Add(obj);
        }

        public void Add(AggregateRoot obj)
        {
            _tracking.Add(obj);
        }

        public void Commit()
        {
            foreach (var aggregate in _tracking)
            {
                _eventStore.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), -1);
            }
        }
    }
}