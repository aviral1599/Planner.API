using MongoDB.Driver;
using PlannerApp.API.Models;

namespace PlannerApp.API.Repositories
{
    public class TaskRepository : MongoRepository<TaskItem>, ITaskRepository
    {
        private readonly IMongoCollection<TaskItem> _collection;
        public TaskRepository(IMongoDatabase db) : base(db, "Tasks")
        {
            _collection = db.GetCollection<TaskItem>("Tasks");
        }

        public async Task<List<TaskItem>> GetByOwnerIdAsync(Guid ownerId) =>
            await _collection.Find(t => t.OwnerId == ownerId).ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(Guid id, Guid ownerId) =>
            await _collection.Find(t => t.Id == id && t.OwnerId == ownerId).FirstOrDefaultAsync();
    }
}
