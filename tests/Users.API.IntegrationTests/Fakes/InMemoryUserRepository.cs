using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Models.Entities;
using Users.Repositories;

namespace Users.API.IntegrationTests.Fakes
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly IReadOnlyCollection<User> _origin;
        private List<User> _users;

        public InMemoryUserRepository() : this(null)
        {

        }

        public InMemoryUserRepository(IReadOnlyCollection<User> users)
        {
            _origin = users ?? new List<User>();
            Reset();
        }

        public void Reset()
        {
            _users = _origin.ToList();
        }

        public async Task<string> GetNonExistentUserId()
        {
            var existingIds = (await GetUsersAsync()).Select(user => user.Id);
            var rnd = new Random();
            string nonExistentId;
            do
            {
                nonExistentId = rnd.Next(int.MaxValue).ToString("D24");
            }
            while (existingIds.Contains(nonExistentId));
            return nonExistentId;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = (_users.Count + 1).ToString("D24");
            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<User> GetUserAsync(string id)
        {
            var user = _users.Where(user => user.Id == id).FirstOrDefault();
            return await Task.FromResult(user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await Task.FromResult(_users.ToList());
        }

        public async Task RemoveUserAsync(User userToRemove)
        {
            _users = _users.Where(user => user.Id != userToRemove.Id).ToList();
            await Task.CompletedTask;
        }

        public async Task RemoveUserAsync(string id)
        {
            _users = _users.Where(user => user.Id != id).ToList();
            await Task.CompletedTask;
        }

        public async Task UpdateUserAsync(string id, User userUpdate)
        {
            var userToUpdate = _users.Where(user => user.Id == userUpdate.Id).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate = userUpdate;
            }
            await Task.CompletedTask;
        }
    }
}
