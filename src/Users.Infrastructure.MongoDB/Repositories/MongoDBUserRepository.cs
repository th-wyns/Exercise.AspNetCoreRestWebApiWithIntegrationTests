using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Infrastructure.MongoDB.Connection;
using Users.Models.Entities;
using Users.Repositories;

namespace Users.Infrastructure.MongoDB.Repositories
{
    public class MongoDBUserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public MongoDBUserRepository(IMongoDBUserRepositoryConnection connection)
        {
            _users = connection.UserCollection;
        }


        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var cursor = await _users.FindAsync(user => true);
                var result = await cursor.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(GetUsersAsync),
                    ErrorType.Infrastructure,
                    innerException: ex);
            }
        }

        public async Task<User> GetUserAsync(string id)
        {
            try
            {
                var cursor = await _users.FindAsync(user => user.Id == id);
                var result = await cursor.FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(GetUserAsync),
                    ErrorType.Infrastructure,
                    innerException: ex);
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                await _users.InsertOneAsync(user);
                return user;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(CreateUserAsync),
                    ErrorType.Infrastructure,
                    innerException: ex);
            }
        }

        public async Task UpdateUserAsync(string id, User user)
        {
            ReplaceOneResult result;

            try
            {
                result = await _users.ReplaceOneAsync(user => user.Id == id, user);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(UpdateUserAsync),
                    ErrorType.Infrastructure,
                    innerException: ex);
            }

            if (result.ModifiedCount < 1)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(UpdateUserAsync),
                    ErrorType.NoMatch,
                    $"{nameof(result.ModifiedCount)}:{result.ModifiedCount}");
            }
        }

        public async Task RemoveUserAsync(User userIn)
        {
            DeleteResult result;

            try
            {
                result = await _users.DeleteOneAsync(user => user.Id == userIn.Id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(RemoveUserAsync),
                    ErrorType.Infrastructure,
                    innerException: ex);
            }

            if (result.DeletedCount < 1)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(RemoveUserAsync),
                    ErrorType.NoMatch,
                    $"{nameof(result.DeletedCount)}:{result.DeletedCount}");
            }
        }

        public async Task RemoveUserAsync(string id)
        {
            var result = await _users.DeleteOneAsync(user => user.Id == id);

            if (result.DeletedCount < 1)
            {
                throw new RepositoryException(
                    nameof(IUserRepository),
                    nameof(RemoveUserAsync),
                    ErrorType.NoMatch,
                    $"{nameof(result.DeletedCount)}:{result.DeletedCount}");
            }
        }
    }
}
