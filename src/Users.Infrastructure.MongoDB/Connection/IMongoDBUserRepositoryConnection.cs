using MongoDB.Driver;
using Users.Models.Entities;

namespace Users.Infrastructure.MongoDB.Connection
{
    public interface IMongoDBUserRepositoryConnection
    {
        IMongoCollection<User> UserCollection { get; }
    }
}
