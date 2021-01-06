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
        [Fact]
        public async Task CreateUser_WithUnsupportedAcceptHeader_ReturnsUnacceptable()
        {
            var createUser = _userRepository.TestUser;

            var response = await HttpRequestBuilder.AcceptUnsupportedType(_httpClient).WithBody(createUser, ContentType.Json).PostAsync();

            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [Fact]
        public async Task CreateUser_WithUnsupportedContentTypeHeader_ReturnsUnsupportedMediaType()
        {
            var createUser = _userRepository.TestUser;

            var response = await HttpRequestBuilder.Default(_httpClient).WithJsonBody(createUser, ContentType.Unsupported).PostAsync();

            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task CreateUser_WithSupportedAcceptHeader_ReturnsExpectedMediaType(AcceptType acceptType, ContentType contentType)
        {
            var createUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(createUser, contentType).PostAsync();

            Assert.Equal(request.MediaTypes.Single(), response.Content.Headers.ContentType.MediaType);
        }


        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task CreateUser_WithValidContent_ReturnsCreatedResult(AcceptType acceptType, ContentType contentType)
        {
            var createUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(createUser, contentType).PostAsync();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.StartsWith($"{_usersControllerBaseUri}", response.Headers.Location.ToString().ToLower());
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task CreateUser_WithValidContent_ReturnsExpectedResult(AcceptType acceptType, ContentType contentType)
        {
            var createUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var (_, model) = await request.WithBody(createUser, contentType).PostAsync<UserDto>();

            Assert.NotNull(model);
            // TODO: compare all the members on the User model to match
            // Assert.Same(userToExpect, model);
            Assert.Equal(createUser.Name, model.Name);
        }


        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task CreateUser_WithValidContent_CreatedContentCanBeRetrieved(AcceptType acceptType, ContentType contentType)
        {
            var createUser = _userRepository.TestUser;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var (response, _) = await request.WithBody(createUser, contentType).PostAsync<UserDto>();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var location = response.Headers.Location.ToString();
            var (getUserResponse, getUserModel) = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync<UserWithLinksDto>(location);

            Assert.Equal(HttpStatusCode.OK, getUserResponse.StatusCode);
            // TODO: compare all the members on the User model to match
            // Assert.Same(createUser, getResponseModel);
            Assert.Equal(createUser.Name, getUserModel.Name);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptAndContentTypeVariations.DataAll), MemberType = typeof(SupportedAcceptAndContentTypeVariations))]
        public async Task CreateUser_WithInalidContent_ReturnsBadRequest(AcceptType acceptType, ContentType contentType)
        {
            var createUser = _userRepository.TestUser;
            createUser.Name = null;

            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.WithBody(createUser, contentType).PostAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.StartsWith("application/problem+", response.Content.Headers.ContentType.MediaType);
        }
    }
}
