using System;
using EventSourcingProtoType.Scheduler.Entities;

namespace EventSourcingProtoType.Scheduler
{
    public interface IRepository<out T> where T : AggregateRoot, new()
    {
        T GetById(Guid id);
    }

    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>() where T:AggregateRoot, new();
    }

    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IEventStore _storage;

        public RepositoryFactory(IEventStore storage)
        {
            _storage = storage;
        }

        public IRepository<T> Create<T>() where T : AggregateRoot, new()
        {
            return new Repository<T>(_storage);
        }
    }

    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public T GetById(Guid id)
        {
            var obj = new T();
            var e = _storage.GetEventsForAggregate(id);
            obj.LoadsFromHistory(e);
            return obj;
        }
    }
}