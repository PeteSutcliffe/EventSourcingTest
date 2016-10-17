using System;
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
    }
}