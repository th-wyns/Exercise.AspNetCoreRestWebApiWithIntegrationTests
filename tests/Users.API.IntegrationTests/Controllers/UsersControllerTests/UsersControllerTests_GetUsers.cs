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
        public async Task GetUsers_ReturnsOkStatusCode(AcceptType acceptType)
        {
            var response = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task GetUsers_ReturnsExpectedMediaType(AcceptType acceptType)
        {
            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.GetAsync();

            Assert.Equal(request.MediaTypes.Single(), response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task GetUsers_WithUnsupportedAcceptHeader_ReturnsUnacceptable()
        {
            var response = await HttpRequestBuilder.AcceptUnsupportedType(_httpClient).GetAsync();

            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task GetUsers_ReturnsExpectedResult(AcceptType acceptType)
        {
            var usersToExpect = await _userRepository.GetUsersAsync();

            var (_, model) = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync<UsersWithLinksDto>();

            Assert.NotNull(model?.Value);
            Assert.NotNull(model?.Links);

            Assert.Equal(usersToExpect.Count, model.Value.Count);
            // TODO: compare all the members on the User models to match
            // Assert.AllSame(usersToExpect, model.Value);
        }
    }
}
