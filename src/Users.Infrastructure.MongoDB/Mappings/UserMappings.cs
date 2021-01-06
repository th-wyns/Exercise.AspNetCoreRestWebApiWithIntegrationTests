using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Users.Models.Entities;

namespace Users.Infrastructure.MongoDB.Mappings
{
    public static class UserMappings
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                // https://mongodb.github.io/mongo-csharp-driver/2.11/reference/bson/mapping/
                cm.MapIdMember(user => user.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            });

            BsonClassMap.RegisterClassMap<Address>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<GeoCoordinate>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<Company>(cm =>
            {
                cm.AutoMap();
            });

        }
    }
}
