using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Users.API.IntegrationTests.Fakes;
using Users.Repositories;

namespace Users.API.IntegrationTests.WebApplicationFactories
{
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public ITestUserRepository UserRepository { get; }

        public TestWebApplicationFactory()
        {
            // TODO: configuration settings option to use original IUserRepository (MongoDB) service or to override and use InMemoryUserRepository
            var userRepositoryToUse = InMemoryUserRepositoryFactory.WithSeedData();
            UserRepository = new TestUserRepository(userRepositoryToUse);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // TODO: create Test settings to override values and use it
            // builder.UseEnvironment("Test");

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IUserRepository>(UserRepository);
            });
        }
    }
}
