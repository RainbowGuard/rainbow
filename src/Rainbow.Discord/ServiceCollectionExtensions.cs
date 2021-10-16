using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Core;
using Rainbow.Discord.Internal;

namespace Rainbow.Discord
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, string token)
        {
            var botConfig = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.All,
            };
            var client = new DiscordSocketClient(botConfig);

            services.AddSingleton(client);
            services.AddSingleton<IBroadcastService, DiscordBroadcastService>();

            var bot = new DiscordBot(client);
            _ = bot.Initialize(token);
            services.AddSingleton(bot);

            return services;
        }
    }
}