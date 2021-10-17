using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rainbow.Configuration;
using Rainbow.Core;
using Rainbow.Discord;
using System;

namespace Rainbow.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((ctx, services) =>
                {
                    services.AddConfiguration(
                        guildConfigOptions => guildConfigOptions.UseSqlite(
                            ctx.Configuration.GetConnectionString("GuildDatabase"),
                            x => x.MigrationsAssembly("Rainbow.Application")));

                    var token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
                    services.AddDiscord(token);

                    services.AddSingleton(FlaggedUserStateDatabase.Create());
                    services.AddSingleton<CoreService>();
                });
    }
}
