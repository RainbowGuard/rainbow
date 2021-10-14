using Microsoft.Extensions.DependencyInjection;
using Rainbow.Core;
using Rainbow.Discord;
using Rainbow.Junkyard;
using System;
using System.Threading.Tasks;

namespace Rainbow.Application
{
    public class Program
    {
        public static async Task Main()
        {
            var services = new ServiceCollection();

            var username = Environment.GetEnvironmentVariable("JUNKYARD_USERNAME");
            var password = Environment.GetEnvironmentVariable("JUNKYARD_PASSWORD");
            var token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

            services.AddJunkyard("https://github.com/RainbowGuard/junkyard.git", username, password);
            services.AddDiscord(token);

            services.AddSingleton<CoreService>();

            await Task.Delay(-1);
        }
    }
}
