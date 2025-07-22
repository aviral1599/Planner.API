using System.Linq.Expressions;

namespace PlannerApp.API.Repositories
{
    public interface IMongoRepository<T> where T : class
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task UpdateAsync(Expression<Func<T, bool>> filter, T entity);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
    }
}
