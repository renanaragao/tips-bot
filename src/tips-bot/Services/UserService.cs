using System.Threading.Tasks;
using tips_bot.Interfaces;
using tips_bot.Models;

namespace tips_bot.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task InsertAsync(User user)
        {
            var found = await repository.GetAsync(user.Id);

            if(found != null) return;

            await repository.InsertAsync(user);
        }
    }
}