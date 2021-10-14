using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Rainbow.Discord.Internal
{
    public class DiscordBot
    {
        private readonly ServiceProvider _services;

        public DiscordBot()
        {
            var botConfig = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.All,
            };

            _services = new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(botConfig))
                .BuildServiceProvider();
        }

        public async Task Initialize()
        {
            var client = _services.GetRequiredService<DiscordSocketClient>();

            client.InteractionCreated += interaction => Task.CompletedTask;

            await client.LoginAsync(TokenType.Bot, "");
        }
    }
}