using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Users.API.IntegrationTests.Helpers;
using Users.API.IntegrationTests.Models;
using Users.Models.DTOs;
using Users.Repositories;
using Xunit;

namespace Users.API.IntegrationTests.Controllers.UsersControllerTests
{
    public partial class UsersControllerTests
    {
        [Fact]
        public async Task PatchUser_WithUnsupportedContentTypeHeader_ReturnsUnsupportedMediaType()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var userPatch = _userRepository.UserPatch;

            var response = await HttpRequestBuilder.Default(_httpClient).WithJsonBody(userPatch, ContentType.Unsupported).PatchAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task PatchUser_WithNonExistentUser_ReturnsNotFound()
        {
            var nonExistentUserId = await _userRepository.GetNonExistentUserId();
            var userPatch = _userRepository.UserPatch;

            var request = HttpRequestBuilder.Default(_httpClient);
            var response = await request.WithJsonBody(userPatch).PatchAsync(nonExistentUserId);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PatchUser_WithValidContent_ReturnsNoContentResult()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var userPatch = _userRepository.UserPatch;

            var request = HttpRequestBuilder.Default(_httpClient);
            var response = await request.WithJsonBody(userPatch).PatchAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task PatchUser_WithValidContent_UpdatesContent()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var userPatch = _userRepository.UserPatch;

            var request = HttpRequestBuilder.Default(_httpClient);
            var response = await request.WithJsonBody(userPatch).PatchAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var (getUserResponse, getUserModel) = await HttpRequestBuilder.AcceptJson(_httpClient).GetAsync<UserWithLinksDto>(existingUser.Id);

            Assert.Equal(HttpStatusCode.OK, getUserResponse.StatusCode);
            // TODO: compare all the changes on the User model to match
            // Assert.Applied(userPatch, getResponseModel);
            Assert.Equal(userPatch.First().Value, getUserModel.Email);
        }


        [Fact]
        public async Task PatchUser_WithInalidContent_ReturnsBadRequest()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var userPatch = _userRepository.UserPatch;
            userPatch.First().Path = "/nonExistentPath";

            var request = HttpRequestBuilder.Default(_httpClient);
            var response = await request.WithJsonBody(userPatch).PatchAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.StartsWith("application/problem+", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task PatchUser_WithNoMatchRepositoryException_ReturnsNotFound()
        {
            _userRepository.ThrowRepositoryException = ErrorType.NoMatch;
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var userPatch = _userRepository.UserPatch;

            var request = HttpRequestBuilder.Default(_httpClient);
            var response = await request.WithJsonBody(userPatch).PatchAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PatchUser_WithInfrastructureRepositoryException_ReturnsInternalServerError()
        {
            _userRepository.ThrowRepositoryException = ErrorType.Infrastructure;
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var userPatch = _userRepository.UserPatch;

            var request = HttpRequestBuilder.Default(_httpClient);
            var response = await request.WithJsonBody(userPatch).PatchAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
