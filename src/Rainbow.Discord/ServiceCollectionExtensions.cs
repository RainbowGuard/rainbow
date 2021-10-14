using Microsoft.Extensions.DependencyInjection;
using Rainbow.Discord.Internal;

namespace Rainbow.Discord
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiscord(this IServiceCollection services)
        {
            var bot = new DiscordBot();
            _ = bot.Initialize();
            services.AddSingleton(bot);
            return services;
        }
    }
}