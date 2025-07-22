using MongoDB.Driver;
using PlannerApp.API.Models;

namespace PlannerApp.API.Repositories
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database) : base(database, "Users")
        {
            _users = database.GetCollection<User>("Users");
        }
        public Task<User?> GetByUsernameAsync(string username)
            => GetAsync(u => u.Username == username);

        public Task<User?> GetByEmailAsync(string email)
            => GetAsync(u => u.Email == email);

        public Task<User?> GetByGoogleIdAsync(string googleId)
            => GetAsync(u => u.GoogleId == googleId);
    }
}
