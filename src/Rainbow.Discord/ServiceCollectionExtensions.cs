using Microsoft.Extensions.DependencyInjection;
using Rainbow.Core;
using Rainbow.Discord.Internal;

namespace Rainbow.Discord
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, string token)
        {
            services.AddSingleton<IBroadcastService, BroadcastService>();

            var bot = new DiscordBot();
            _ = bot.Initialize(token);
            services.AddSingleton(bot);

            return services;
        }
    }
}