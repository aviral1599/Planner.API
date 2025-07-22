using PlannerApp.API.Models;

namespace PlannerApp.API.Repositories
{
    public interface IUserRepository : IMongoRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByGoogleIdAsync(string googleId);
    }
}
