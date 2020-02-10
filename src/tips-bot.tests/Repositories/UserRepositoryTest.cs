using System.Threading.Tasks;
using AutoFixture;
using CompareObject;
using MongoDB.Driver;
using tips_bot.Interfaces;
using tips_bot.Models;
using tips_bot.Repository;
using Xunit;

namespace tips_bot.tests.Repositories
{
    public class UserRepositoryTest : IClassFixture<MongoIntegrationTest.MongoIntegrationTest>
    {
        private readonly IUserRepository repository;
        private readonly Fixture fixture;
        private User user;
        private User otherUser;
        private readonly IMongoCollection<User> collection;

        // private readonly MongoCollectio
        public UserRepositoryTest(MongoIntegrationTest.MongoIntegrationTest integrationTest)
        {
            collection = integrationTest.GetCollection<User>();
            fixture = new Fixture();

            repository = new UserRepository(integrationTest.Database);
        }

        [Fact]
        public async Task Should_Insert_User()
        {
            try
            {
                user = new User
                (
                    id: fixture.Create<int>(),
                    username: fixture.Create<string>(),
                    firstName: fixture.Create<string>(),
                    lastName: fixture.Create<string>()
                );

                await repository.InsertAsync(user);

                var found = await collection.Find(x => x.Id == user.Id).FirstOrDefaultAsync();

                Assert.Equal(user.FirstName, found.FirstName);
                Assert.True(user.Compare(found));
            }
            finally
            {
                await DeleteAll();
            }
        }

        [Fact]
        public async Task Should_Not_Insert_User_If_Is_Null()
        {
            try
            {
                user = new User
                (
                    id: fixture.Create<int>(),
                    username: fixture.Create<string>(),
                    firstName: fixture.Create<string>(),
                    lastName: fixture.Create<string>()
                );

                await repository.InsertAsync(user);

                var found = await collection.Find(x => x.Id == user.Id).FirstOrDefaultAsync();

                Assert.Equal(user.FirstName, found.FirstName);
                Assert.True(user.Compare(found));
            }
            finally
            {
                await DeleteAll();
            }
        } 

        [Fact]
        public async Task Should_Get_User_By_Id()
        {
            try
            {
                user = new User
                (
                    id: fixture.Create<int>(),
                    username: fixture.Create<string>(),
                    firstName: fixture.Create<string>(),
                    lastName: fixture.Create<string>()
                );

                await repository.InsertAsync(user);

                var userResult = await repository.GetAsync(user.Id);

                Assert.Equal(userResult.Id, user.Id);
                Assert.True(user.Compare(userResult));
            }
            finally
            {
                await DeleteAll();
            }
        }

        [Fact]
        public async Task Should_NotGet_User_By_Id_And_Return_Null()
        {
            try
            {
                user = new User
                (
                    id: fixture.Create<int>(),
                    username: fixture.Create<string>(),
                    firstName: fixture.Create<string>(),
                    lastName: fixture.Create<string>()
                );

                otherUser = new User
                (
                    id: fixture.Create<int>(),
                    username: fixture.Create<string>(),
                    firstName: fixture.Create<string>(),
                    lastName: fixture.Create<string>()
                );

                await repository.InsertAsync(user);

                var userResult = await repository.GetAsync(otherUser.Id);

                Assert.Null(userResult);
            }
            finally
            {
                await DeleteAll();
            }
        }

        private async Task DeleteAll()
        {
            await collection.DeleteManyAsync(Builders<User>.Filter.Empty);
        }
    }
}