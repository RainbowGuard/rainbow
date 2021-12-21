using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Services.Discord;
using Rainbow.Services.Logging;
using System;
using System.Threading.Tasks;
using Rainbow.Database;

var client = new DiscordSocketClient(new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.All &
                     ~GatewayIntents.GuildPresences &
                     ~GatewayIntents.GuildScheduledEvents &
                     ~GatewayIntents.GuildInvites,
});

var services = new ServiceCollection()
    .AddDbContext<RainbowContext>()
    .AddSingleton(client)
    .AddSingleton<Logger>()
    .AddSingleton<CommandService>()
    .AddSingleton<CommandHandler>()
    .BuildServiceProvider();

await services.GetRequiredService<CommandHandler>().InstallCommandsAsync();

client.Log += services.GetRequiredService<Logger>().LogAsync;

await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN"));
await client.StartAsync();

await Task.Delay(-1);
