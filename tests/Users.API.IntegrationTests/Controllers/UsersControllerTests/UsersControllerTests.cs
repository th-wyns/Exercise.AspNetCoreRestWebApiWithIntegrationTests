using System;
using System.Net.Http;
using Users.API.IntegrationTests.Fakes;
using Users.API.IntegrationTests.WebApplicationFactories;
using Xunit;

namespace Users.API.IntegrationTests.Controllers.UsersControllerTests
{
    public partial class UsersControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private readonly ITestUserRepository _userRepository;
        private readonly string _usersControllerBaseUri = "https://localhost/api/users/";

        public UsersControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri(_usersControllerBaseUri);
            _httpClient = factory.CreateClient();
            _userRepository = factory.UserRepository;
            _userRepository.Reset();
        }
    }
}
