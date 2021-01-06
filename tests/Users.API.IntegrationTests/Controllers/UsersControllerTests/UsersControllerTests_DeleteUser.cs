using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Users.API.IntegrationTests.Helpers;
using Users.Repositories;
using Xunit;

namespace Users.API.IntegrationTests.Controllers.UsersControllerTests
{
    public partial class UsersControllerTests
    {
        [Fact]
        public async Task DeleteUser_WithNonExistentUser_ReturnsNotFound()
        {
            var nonExistentUserId = await _userRepository.GetNonExistentUserId();

            var response = await HttpRequestBuilder.Default(_httpClient).DeleteAsync(nonExistentUserId);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_WithExistingUser_ReturnsNoContentResult()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();

            var response = await HttpRequestBuilder.Default(_httpClient).DeleteAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_WithExistingUser_RemovesContent()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();

            var _ = await HttpRequestBuilder.Default(_httpClient).DeleteAsync(existingUser.Id);

            var getUserResponse = await HttpRequestBuilder.Default(_httpClient).GetAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NotFound, getUserResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_WithNoMatchRepositoryException_ReturnsNotFound()
        {
            _userRepository.ThrowRepositoryException = ErrorType.NoMatch;
            var existingUser = (await _userRepository.GetUsersAsync()).First();

            var response = await HttpRequestBuilder.Default(_httpClient).DeleteAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_WithInfrastructureRepositoryException_ReturnsInternalServerError()
        {
            _userRepository.ThrowRepositoryException = ErrorType.Infrastructure;
            var existingUser = (await _userRepository.GetUsersAsync()).First();

            var response = await HttpRequestBuilder.Default(_httpClient).DeleteAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
