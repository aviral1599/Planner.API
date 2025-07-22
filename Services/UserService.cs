using Microsoft.AspNetCore.Identity;
using PlannerApp.API.Models;
using PlannerApp.API.Repositories;

namespace PlannerApp.API.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<User> _hasher = new PasswordHasher<User>();
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> UsernameOrEmailExistsAsync(string username, string email)
            => (await _userRepo.GetByUsernameAsync(username)) != null
            || (await _userRepo.GetByEmailAsync(email)) != null;

        public async Task<User> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {   
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Username = dto.Name,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            await _userRepo.AddAsync(user);
            return user;
        }

        public async Task<User?> ValidateLoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null || user.PasswordHash is null)
                return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<User> GoogleLoginAsync(string googleId, string name, string email)
        {
            var user = await _userRepo.GetByGoogleIdAsync(googleId);
            if (user == null)
            {
                user = new User
                {
                    Name = name,
                    Email = email,
                    Username = email,
                    GoogleId = googleId,
                    CreatedAt = DateTime.UtcNow
                };
                await _userRepo.AddAsync(user);
            }
            return user;
        }
    }
}
