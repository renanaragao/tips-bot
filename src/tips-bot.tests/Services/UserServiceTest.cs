using System.Threading.Tasks;
using AutoFixture;
using NSubstitute;
using tips_bot.Interfaces;
using tips_bot.Models;
using tips_bot.Services;
using Xunit;

namespace tips_bot.tests.Services
{
    public class UserServiceTest
    {
        private readonly User user;
        private readonly Fixture fixture;
        private readonly IUserRepository repository;
        private readonly IUserService service;

        public UserServiceTest()
        {
            fixture = new Fixture();
            user = new User
                (
                    id: fixture.Create<int>(),
                    username: fixture.Create<string>(),
                    firstName: fixture.Create<string>(),
                    lastName: fixture.Create<string>()
                );

            repository = Substitute.For<IUserRepository>();
            repository.GetAsync(Arg.Any<int>()).Returns(user);
            repository.InsertAsync(Arg.Any<User>()).Returns(Task.CompletedTask);

            service = new UserService(repository);
        }

        [Fact]
        public async Task Should_Not_Insert_User()
        {
            await service.InsertAsync(user);

            await repository.Received().GetAsync(user.Id);

            await repository.DidNotReceive().InsertAsync(Arg.Any<User>());

        }
    }
}