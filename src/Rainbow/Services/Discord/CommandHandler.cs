using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Rainbow.Contexts;
using Rainbow.Entities;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Rainbow.Services.Discord;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly RainbowContext _context;
    private readonly IServiceProvider _services;

    public CommandHandler(IServiceProvider services, DiscordSocketClient client, RainbowContext context, CommandService commands)
    {
        _commands = commands;
        _services = services;
        _context = context;
        _client = client;
    }

    public async Task InstallCommandsAsync()
    {
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                        services: _services);
        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        if (messageParam is not SocketUserMessage message) return;

        // Read the command prefix
        var argPos = 0;
        var prefix = GuildConfiguration.DefaultPrefix;
        if (message.Channel is SocketGuildChannel channel)
        {
            // Get the guild's configuration
            var config = await _context.GuildConfigurations
                .Include(s => s.Id)
                .Include(s => s.Prefix)
                .FirstOrDefaultAsync(c => c.Id == channel.Guild.Id);
            if (config == null)
            {
                // Create a new guild configuration
                config = new GuildConfiguration { Id = channel.Guild.Id };
                _context.Add(config);
                await _context.SaveChangesAsync();
            }

            prefix = config.Prefix;
        }

        if (!(message.HasStringPrefix(prefix, ref argPos) ||
              message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(_client, message);

        // Execute the command with the command context we just
        // created, along with the service provider for precondition checks.
        await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: null);
    }
}