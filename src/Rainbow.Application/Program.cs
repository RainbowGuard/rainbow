using Microsoft.Extensions.DependencyInjection;
using Rainbow.Core;
using Rainbow.Discord;
using System;
using System.Threading.Tasks;

namespace Rainbow.Application
{
    public class Program
    {
        public static async Task Main()
        {
            var services = new ServiceCollection();

            var token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            services.AddDiscord(token);

            services.AddSingleton<CoreService>();

            await Task.Delay(-1);
        }
    }
}
