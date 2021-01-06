using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Users.API.IntegrationTests.Data;
using Users.API.IntegrationTests.Helpers;
using Users.API.IntegrationTests.Models;
using Users.Models.DTOs;
using Xunit;

namespace Users.API.IntegrationTests.Controllers.RootControllerTests
{
    public class RootControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public RootControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("https://localhost/api/");
            _httpClient = factory.CreateClient();
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task Root_ReturnsOkStatusCode(AcceptType acceptType)
        {
            var response = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task Root_WithSupportedAcceptHeader_ReturnsExpectedMediaType(AcceptType acceptType)
        {
            var request = HttpRequestBuilder.Accept(_httpClient, acceptType);
            var response = await request.GetAsync();

            Assert.Equal(request.MediaTypes.Single(), response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task Root_WithUnsupportedAcceptHeader_ReturnsUnacceptable()
        {
            var response = await HttpRequestBuilder.AcceptUnsupportedType(_httpClient).GetAsync();

            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(SupportedAcceptTypes.DataAll), MemberType = typeof(SupportedAcceptTypes))]
        public async Task Root_ReturnsExpectedResult(AcceptType acceptType)
        {
            var (_, model) = await HttpRequestBuilder.Accept(_httpClient, acceptType).GetAsync<List<LinkDto>>();

            Assert.NotNull(model);
            Assert.NotEmpty(model);

            // TODO: thorough link testing
            // Assert.LinksCorrect(linksToExpect, model);
        }
    }
}
