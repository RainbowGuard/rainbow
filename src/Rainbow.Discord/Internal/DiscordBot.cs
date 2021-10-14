using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Discord.Internal.Events;
using Rainbow.Discord.Internal.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rainbow.Discord.Internal
{
    public class DiscordBot
    {
        private readonly ServiceProvider _services;

        public DiscordBot(DiscordSocketClient client)
        {
            _services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(typeof(DiscordBot).Assembly
                    .GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(SlashCommand)))
                    .Select(t => (SlashCommand)Activator.CreateInstance(t)))
                .BuildServiceProvider();
        }

        public async Task Initialize(string token)
        {
            var client = _services.GetRequiredService<DiscordSocketClient>();
            var slashCommands = _services.GetRequiredService<IEnumerable<SlashCommand>>();

            client.Ready += () => ClientReady.Handler(client);
            client.InteractionCreated += interaction => InteractionCreated.Handler(client, interaction, slashCommands, _services);

            await client.LoginAsync(TokenType.Bot, token);
        }
    }
}