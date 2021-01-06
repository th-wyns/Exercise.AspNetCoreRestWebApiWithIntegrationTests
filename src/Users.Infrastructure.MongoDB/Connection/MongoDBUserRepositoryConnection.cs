using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Users.Infrastructure.MongoDB.Mappings;
using Users.Models.Entities;
using Users.Models.Settings;

namespace Users.Infrastructure.MongoDB.Connection
{
    public class MongoDBUserRepositoryConnection : IMongoDBUserRepositoryConnection
    {
        public IMongoCollection<User> UserCollection { get; private set; }

        public MongoDBUserRepositoryConnection(IUserRepositorySettings settings)
        {
            Setup();
            Create(settings);
        }

        private static void Setup()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            UserMappings.Map();
        }

        private void Create(IUserRepositorySettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            UserCollection = database.GetCollection<User>(settings.CollectionName);
        }
    }
}
