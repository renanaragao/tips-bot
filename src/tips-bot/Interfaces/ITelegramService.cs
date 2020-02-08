using System.Collections.Generic;
using System.Threading.Tasks;
using tips_bot.Models;

namespace tips_bot.Interfaces
{
    public interface ITelegramService
    {
         Task<IEnumerable<Update>> GetUpdatesAsync();
         Task SendMessage(Message message);
    }
}