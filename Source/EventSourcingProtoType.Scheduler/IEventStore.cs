using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingProtoType.Scheduler.Events;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;

namespace EventSourcingProtoType.Scheduler
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }

    public class EventStore : IEventStore
    {
        private readonly IEventPublisher _publisher;

        [BsonIgnoreExtraElements]
        public class SavedEvent
        {
            public int Version { get; set; }
            public string EventType { get; set; }
            public string SerializedData { get; set; }
            public Guid AggegateId { get; set; }
        }

        static EventStore()
        {
            var connstringBuilder =
                new MongoUrlBuilder("mongodb://localhost")
                {
                    DatabaseName = "EventStore"
                };
            var settings = MongoClientSettings.FromUrl(connstringBuilder.ToMongoUrl());
            var client = new MongoClient(settings);
            Database = client.GetDatabase("EventStore");
        }

        public EventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
            _collection = Database.GetCollection<SavedEvent>("Events");
        }

        private static readonly IMongoDatabase Database;
        private readonly IMongoCollection<SavedEvent> _collection;

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            var lastEventVersion = _collection.AsQueryable()
                .Where(c => c.AggegateId == aggregateId)
                .OrderByDescending(e => e.Version)
                .Select(e => e.Version)
                .FirstOrDefault();

            if (lastEventVersion != expectedVersion && expectedVersion != -1)
            {
                throw new ConcurrencyException();
            }

            var i = expectedVersion;

            // iterate through current aggregate events increasing version with each processed event
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;

                _collection.InsertOne(new SavedEvent
                {
                    AggegateId = aggregateId,
                    EventType = @event.GetType().AssemblyQualifiedName,
                    Version = @event.Version,
                    SerializedData = JsonConvert.SerializeObject(@event)
                });

                // publish current event to the bus for further processing by subscribers
                _publisher.Publish(@event);
            }
        }

        // collect all processed events for given aggregate and return them as a list
        // used to build up an aggregate from its history (Domain.LoadsFromHistory)
        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            var events = _collection.AsQueryable()
                .Where(c => c.AggegateId == aggregateId)
                .OrderBy(e => e.Version);

            var retEvents = new List<Event>();

            foreach (var savedEvent in events)
            {
                var t = Type.GetType(savedEvent.EventType, true);
                var @event = (Event)JsonConvert.DeserializeObject(savedEvent.SerializedData, t);
                retEvents.Add(@event);
            }

            return retEvents;
        }
    }

    public class AggregateNotFoundException : Exception
    {
    }

    public class ConcurrencyException : Exception
    {
    }
}