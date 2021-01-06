using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.IntegrationTests.Models;
using Users.Models.DTOs;
using Users.Models.Entities;
using Users.Repositories;

namespace Users.API.IntegrationTests.Fakes
{
    public class TestUserRepository : ITestUserRepository
    {
        private readonly IUserRepository _userRepository;

        public ErrorType? ThrowRepositoryException { get; set; } = null;

        public TestUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region IUserRepository interface delegations
        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> GetUserAsync(string id)
        {
            return await _userRepository.GetUserAsync(id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task RemoveUserAsync(User userToRemove)
        {
            if (ThrowRepositoryException.HasValue)
            {
                throw new RepositoryException(null, null, ThrowRepositoryException.Value);
            }

            await _userRepository.RemoveUserAsync(userToRemove);
        }

        public async Task RemoveUserAsync(string id)
        {
            if (ThrowRepositoryException.HasValue)
            {
                throw new RepositoryException(null, null, ThrowRepositoryException.Value);
            }

            await _userRepository.RemoveUserAsync(id);
        }

        public async Task UpdateUserAsync(string id, User userUpdate)
        {
            if (ThrowRepositoryException.HasValue)
            {
                throw new RepositoryException(null, null, ThrowRepositoryException.Value);
            }

            await _userRepository.UpdateUserAsync(id, userUpdate);
        }
        #endregion

        public void Reset()
        {
            // TODO: refactor to more flexible code to work with MongoDBUserRepository reset operation as well
            if (_userRepository is InMemoryUserRepository inMemoryUserRepository)
            {
                inMemoryUserRepository.Reset();
            }

            ThrowRepositoryException = null;
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

        public UserCreateDto TestUser
        {
            get
            {
                return new UserCreateDto
                {
                    //Id = "100000000000000000000001",
                    Name = "Test Test",
                    UserName = "Test",
                    Email = "test@test.me",
                    Address = new AddressDto
                    {
                        Street = "Street",
                        Suite = "Suite 4",
                        City = "City",
                        Zipcode = "0001",
                        Geo = new GeoCoordinateDto
                        {
                            Lat = -11.1111,
                            Lng = 44.4444
                        }
                    },
                    Phone = "1-23-456-7890",
                    Website = "test.org",
                    Company = new CompanyDto
                    {
                        Name = "Test Co.",
                        CatchPhrase = "Testing is fun",
                        Bs = "test test test"
                    }
                };
            }
        }

        public List<JsonPatchOperation> UserPatch
        {
            get
            {
                return new List<JsonPatchOperation>()
                {
                    new JsonPatchOperation()
                    {
                        Op = "replace",
                        Path = "/email",
                        Value = "test.new.patched@email.value"
                    }
                };
            }
        }
    }
}
