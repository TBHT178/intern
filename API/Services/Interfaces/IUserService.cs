using CloudinaryDotNet.Actions;
using API.Entity;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<string> GenerateJwtToken(User user);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        bool IsAdmin(User user);
        bool IsPilot(User user);
        bool IsCrew(User user);
        Task SaveRefreshTokenAsync(int userId, string refreshToken);
        Task<string> GetRefreshTokenAsync(int userId);
        
        Task<bool> DisableUserByEmailAsync(string email);
        Task<bool> EnableUserByEmailAsync(string email); 
    }

}