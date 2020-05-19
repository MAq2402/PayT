using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PayT.Infrastructure.Repositories
{
    public class ReadRepository : IReadRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public ReadRepository()
        {
            
        }

        public void InsertOne<T>(T entity)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("PayT");
            var data = _database.GetCollection<T>("PayT");
            data.InsertOne(entity);
        }

        public IEnumerable<T> GetAll<T>()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("PayT");
            var data = _database.GetCollection<T>("PayT");

            var readAll = data.Find(new BsonDocument());

            return readAll.ToEnumerable();
        }
    }
}
