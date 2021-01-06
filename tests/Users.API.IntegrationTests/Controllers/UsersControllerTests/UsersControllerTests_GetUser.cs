using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Users.API.IntegrationTests.Data;
using Users.API.IntegrationTests.Helpers;
using Users.API.IntegrationTests.Models;
using Users.Models.DTOs;
using Xunit;

namespace Users.API.IntegrationTests.Controllers.UsersControllerTests
{
    public partial class UsersControllerTests
    {
        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task GetUser_WithExistingUser_ReturnsOkStatusCode(AcceptType acceptType)
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();

            var response = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUser_WithNonExistentUser_ReturnsNotFoundStatusCode()
        {
            var nonExistentUserId = await _userRepository.GetNonExistentUserId();

            var response = await _httpClient.GetAsync(nonExistentUserId);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task GetUser_WithSupportedAcceptHeader_ReturnsExpectedMediaType(AcceptType acceptType)
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.GetAsync(existingUser.Id);

            Assert.Equal(request.MediaTypes.Single(), response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetUser_WithUnsupportedAcceptHeader_ReturnsUnacceptable()
        {
            var existingUser = (await _userRepository.GetUsersAsync()).First();

            var response = await HttpRequestBuilder.AcceptUnsupportedType(_httpClient).GetAsync(existingUser.Id);

            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task GetUser_WithExistingUser_ReturnsExpectedResult(AcceptType acceptType)
        {
            var userToExpect = (await _userRepository.GetUsersAsync()).First();

            var (_, model) = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync<UserWithLinksDto>(userToExpect.Id);

            Assert.NotNull(model);
            Assert.NotNull(model.Links);
            Assert.Equal(userToExpect.Id, model.Id);
            // TODO: compare all the members on the User model to match
            // Assert.Same(userToExpect, model);
        }
    }
}
