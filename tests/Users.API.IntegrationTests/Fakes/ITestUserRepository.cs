using System.Collections.Generic;
using System.Threading.Tasks;
using Users.API.IntegrationTests.Models;
using Users.Models.DTOs;
using Users.Repositories;

namespace Users.API.IntegrationTests.Fakes
{
    public interface ITestUserRepository : IUserRepository
    {
        void Reset();
        Task<string> GetNonExistentUserId();
        UserCreateDto TestUser { get; }
        public List<JsonPatchOperation> UserPatch { get; }
        public ErrorType? ThrowRepositoryException { get; set; }
    }
}
