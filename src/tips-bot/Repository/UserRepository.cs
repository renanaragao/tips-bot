using System.Threading.Tasks;
using MongoDB.Driver;
using tips_bot.Interfaces;
using tips_bot.Models;

namespace tips_bot.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> collection;

        public UserRepository(IMongoDatabase dataBase)
        {
            collection = dataBase.GetCollection<User>(nameof(User));
        }
        public async Task Insert(User user) 
        => await collection.InsertOneAsync(user);
    }
}