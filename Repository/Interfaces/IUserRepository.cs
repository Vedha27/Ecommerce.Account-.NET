using Entities.Domain_Models;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);

        Task<User?> LoginAsync(string email, string password);

        Task<User?> GetUserByEmailAsync(string email);

    }
}
