using System.Threading.Tasks;
using MongoDB.Driver;
using tips_bot.Models;

namespace tips_bot.Repository
{
    public class TipsRepository
    {
        private readonly IMongoCollection<Models.Tip> collection;

        public TipsRepository(IMongoDatabase dataBase)
        {
            collection = dataBase.GetCollection<Models.Tip>(nameof(Models.Tip));
        }
        async Task InsertAsync(Models.Tip obj)
        => await collection.InsertOneAsync(obj);
    }
}