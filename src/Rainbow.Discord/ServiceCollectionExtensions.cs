using Microsoft.Extensions.DependencyInjection;
using Rainbow.Discord.Internal;

namespace Rainbow.Discord
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services, string token)
        {
            var bot = new DiscordBot();
            _ = bot.Initialize(token);
            services.AddSingleton(bot);
            return services;
        }
    }
}