using System.Threading.Tasks;
using tips_bot.Models;

namespace tips_bot.Interfaces
{
    public interface IUserService
    {
         Task InsertAsync(User user);
    }
}