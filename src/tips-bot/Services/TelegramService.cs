using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using tips_bot.Interfaces;
using tips_bot.Models;

namespace tips_bot.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly string host;
        private readonly string token;

        public TelegramService(string host, string token)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new System.ArgumentException("message", nameof(host));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new System.ArgumentException("message", nameof(token));
            }

            this.host = host;
            this.token = token;
        }

        public async Task<IEnumerable<Update>> GetUpdatesAsync()
        {
            var result = await host.AppendPathSegment(token)
            .SetQueryParam("allow_updates", @"[""message""]")
            .GetJsonAsync<ResultTelegram>();

            return result.Result;
        }

        public async Task SendMessage(Message message)
        {
            await host.AppendPathSegment($"{token}/sendMessage")
                .SetQueryParam("chat_id", message.Id)
                .SetQueryParam("text", message.Text)
                .GetAsync();
        }

        public struct ResultTelegram
        {
            public IEnumerable<Update> Result { get; set; }
        }
    }
}