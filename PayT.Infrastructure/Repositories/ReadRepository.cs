using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using PayT.Infrastructure.Types;

namespace PayT.Infrastructure.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : IReadModel
    {
        protected readonly IMongoCollection<T> _collection;

        public ReadRepository()
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            var client = new MongoClient();
            var database = client.GetDatabase("PayT");
            _collection = database.GetCollection<T>("PayT");
        }

        public async Task InsertOneAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _collection.FindAsync(new BsonDocument());

            return result.ToEnumerable();
        }
    }
}
