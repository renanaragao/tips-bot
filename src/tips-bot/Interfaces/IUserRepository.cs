using System.Threading.Tasks;
using tips_bot.Models;

namespace tips_bot.Interfaces
{
    public interface IUserRepository
    {
         Task InsertAsync(User user);
        Task<User> GetAsync(int id);
    }
}