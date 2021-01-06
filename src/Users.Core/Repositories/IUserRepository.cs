using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Models.Entities;

namespace Users.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(string id, User user);
        Task RemoveUserAsync(User user);
        Task RemoveUserAsync(string id);
    }
}
