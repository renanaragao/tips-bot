using System.Linq;
using System.Net.Http;
using System;
using Flurl.Http.Testing;
using tips_bot.Interfaces;
using tips_bot.Services;
using Xunit;
using System.Collections.Generic;
using tips_bot.Models;
using System.Threading.Tasks;
using AutoFixture;
using CompareObject;

namespace tips_bot.tests.Services
{
    public class TelegramServiceTest : IDisposable
    {
        private readonly HttpTest httpTest;
        private readonly IEnumerable<Update> updates;
        private readonly TelegramService.ResultTelegram resultTelegram;  
        private readonly string host = "http://some.com";
        private readonly string token = Guid.NewGuid().ToString();
        private readonly ITelegramService service;
        private bool disposedValue = false;
        private readonly Fixture fixture; 

        public TelegramServiceTest()
        {
            fixture = new Fixture();
            httpTest = new HttpTest();
            updates = GetUpdates();
            resultTelegram = new TelegramService.ResultTelegram 
            {
                Result = updates
            };

            service = new TelegramService(host, token);
        }

        private IEnumerable<Update> GetUpdates()
        => new [] 
        {
            GetUpdate(),
            GetUpdate(),
            GetUpdate(),
            GetUpdate()
        };

        private Update GetUpdate()
        => new Update
        (
            new Message (
                id: fixture.Create<int>(),
                from: new User
                (
                    id: fixture.Create<int>(),
                    username: fixture.Create<string>(),
                    firstName: fixture.Create<string>(),
                    lastName: fixture.Create<string>()
                ),
                date: fixture.Create<int>(),
                text: fixture.Create<string>()
            )
        );

        [Fact]
        public async Task Should_Get_Updates()
        {
            httpTest.RespondWithJson(resultTelegram);

            var result = await service.GetUpdatesAsync();

            Assert.True(updates.Compare(result));

            httpTest.ShouldHaveCalled($"{host}/{token}")
                .WithVerb(HttpMethod.Get)
                .WithQueryParamValue("allow_updates", @"[""message""]")
                .Times(1);
        }

        [Fact]
        public async Task Should_Send_Message()
        {
            var user = new User(0, "username", "firstName", "lastName");
            var message = new Message(0, user, 0, "text");

            httpTest.RespondWithJson(resultTelegram);

            await service.SendMessage(message);

            httpTest.ShouldHaveCalled($"{host}/{token}/sendMessage")
                .WithVerb(HttpMethod.Get)
                .WithQueryParamValue("chat_id", message.Id)
                .WithQueryParamValue("text", message.Text)
                .Times(1);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    httpTest.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}