using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Users.API.IntegrationTests.Data;
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
        public async Task UpdateUser_WithUnsupportedContentTypeHeader_ReturnsUnsupportedMediaType()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var updateUser = _userRepository.TestUser;

            var response = await HttpRequestBuilder.Default(_httpClient).WithJsonBody(updateUser, ContentType.Unsupported).PutAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task UpdateUser_WithNonExistentUser_ReturnsNotFound(AcceptType acceptType, ContentType contentType)
        {
            var nonExistentUserId = await _userRepository.GetNonExistentUserId();
            var updateUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(updateUser, contentType).PutAsync(nonExistentUserId);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task UpdateUser_WithValidContent_ReturnsNoContentResult(AcceptType acceptType, ContentType contentType)
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var updateUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(updateUser, contentType).PutAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task UpdateUser_WithValidContent_UpdatesContent(AcceptType acceptType, ContentType contentType)
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var updateUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(updateUser, contentType).PutAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var (getUserResponse, getUserModel) = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync<UserWithLinksDto>(existingUser.Id);

            Assert.Equal(HttpStatusCode.OK, getUserResponse.StatusCode);
            // TODO: compare all the members on the User model to match
            // Assert.Same(updateUser, getResponseModel);
            Assert.Equal(updateUser.Name, getUserModel.Name);
        }


        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task UpdateUser_WithInalidContent_ReturnsBadRequest(AcceptType acceptType, ContentType contentType)
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var updateUser = _userRepository.TestUser;
            updateUser.Name = null;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(updateUser, contentType).PutAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.StartsWith("application/problem+", response.Content.Headers.ContentType.MediaType);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task UpdateUser_WithNoMatchRepositoryException_ReturnsNotFound(AcceptType acceptType, ContentType contentType)
        {
            _userRepository.ThrowRepositoryException = ErrorType.NoMatch;
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var updateUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(updateUser, contentType).PutAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task UpdateUser_WithInfrastructureRepositoryException_ReturnsInternalServerError(AcceptType acceptType, ContentType contentType)
        {
            _userRepository.ThrowRepositoryException = ErrorType.Infrastructure;
            var existingUser = (await _userRepository.GetUsersAsync()).First();
            var updateUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(updateUser, contentType).PutAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
