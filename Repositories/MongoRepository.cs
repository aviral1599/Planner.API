using MongoDB.Driver;
using System.Linq.Expressions;

namespace PlannerApp.API.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
            => await _collection.Find(filter).FirstOrDefaultAsync();

        public async Task<List<T>> GetManyAsync(Expression<Func<T, bool>> filter)
            => await _collection.Find(filter).ToListAsync();

        public async Task AddAsync(T entity) => await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(Expression<Func<T, bool>> filter, T entity)
            => await _collection.ReplaceOneAsync(filter, entity);

        public async Task DeleteAsync(Expression<Func<T, bool>> filter)
            => await _collection.DeleteOneAsync(filter);
    }
}
