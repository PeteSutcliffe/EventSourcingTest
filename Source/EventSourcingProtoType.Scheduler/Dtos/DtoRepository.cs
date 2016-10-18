using System;
using System.Linq;
using MongoDB.Driver;

namespace EventSourcingProtoType.Scheduler.Dtos
{
    public class DtoRepository
    {
        private static readonly IMongoDatabase Database;

        static DtoRepository()
        {
            var connstringBuilder =
                new MongoUrlBuilder("mongodb://localhost");

            var settings = MongoClientSettings.FromUrl(connstringBuilder.ToMongoUrl());
            var client = new MongoClient(settings);
            Database = client.GetDatabase("Dtos");
        }

        public T Get<T>(Guid id) where T : Dto
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            return collection.AsQueryable().SingleOrDefault(a => a.Id == id);
        }

        public IQueryable<T> AsQueryable<T>()
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            return collection.AsQueryable();
        } 

        public void Add<T>(T item)
        {
            var collection = Database.GetCollection<T>(typeof (T).Name);
            collection.InsertOne(item);
        }

        public void Update<T>(Guid id, UpdateDefinition<T> updateDef) where T:Dto
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            collection.UpdateOne(i => i.Id == id, updateDef);
        }

        public void Upsert<T>(T item) where T:Dto
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            collection.ReplaceOne(i => i.Id == item.Id, item, new UpdateOptions() { IsUpsert = true });
        }
    }
}