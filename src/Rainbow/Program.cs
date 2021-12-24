using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Database;
using Rainbow.Services.Discord;
using Rainbow.Services.Flagging;
using Rainbow.Services.Logging;
using System;
using System.Threading.Tasks;
using Rainbow.Interactions;

var client = new DiscordSocketClient(new DiscordSocketConfig
{
    // This warns about an unused guild presences intent, but it's
    // required to fetch server members, for some reason
    GatewayIntents = GatewayIntents.All ^
                     GatewayIntents.GuildScheduledEvents ^
                     GatewayIntents.GuildInvites,
});

var services = new ServiceCollection()
    .AddSingleton(client)
    .AddSingleton<CommandService>()
    .AddSingleton<CommandHandler>()
    .AddSingleton<InteractionHandler>()
    .AddSingleton<Logger>()
    .AddSingleton<RevokeFlagBanBlipHandler>()
    .AddSingleton<UserFlags>()
    .AddDbContext<RainbowContext>()
    .BuildServiceProvider();

await services.GetRequiredService<CommandHandler>().InstallCommandsAsync();

client.Log += services.GetRequiredService<Logger>().LogAsync;
client.ButtonExecuted += services.GetRequiredService<InteractionHandler>().HandleBlip;

await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN"));
await client.StartAsync();

await Task.Delay(-1);
