using PlannerApp.API.Models;

namespace PlannerApp.API.Repositories
{
    public interface ITaskRepository : IMongoRepository<TaskItem>
    {
        Task<List<TaskItem>> GetByOwnerIdAsync(Guid ownerId);
        Task<TaskItem?> GetByIdAsync(Guid id, Guid ownerId); // Secure by owner/user
    }
}
