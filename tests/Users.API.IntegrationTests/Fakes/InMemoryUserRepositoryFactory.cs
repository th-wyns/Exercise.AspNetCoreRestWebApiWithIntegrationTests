using System.Collections.Generic;
using Users.Models.Entities;

namespace Users.API.IntegrationTests.Fakes
{
    public static class InMemoryUserRepositoryFactory
    {
        public static InMemoryUserRepository WithCustomData(IReadOnlyCollection<User> users = null)
        {
            return new InMemoryUserRepository(users);
        }

        public static InMemoryUserRepository WithSeedData()
        {
            var seedData = Seed.UserData.Get();
            return new InMemoryUserRepository(seedData);
        }
    }
}
