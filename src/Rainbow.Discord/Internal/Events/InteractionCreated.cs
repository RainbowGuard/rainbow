using System;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Discord.Internal.SlashCommands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rainbow.Discord.Internal.Events
{
    internal static class InteractionCreated
    {
        public static async Task Handler(DiscordSocketClient client, SocketInteraction interaction, IEnumerable<SlashCommand> slashCommands, ServiceProvider services)
        {
            if (interaction is SocketSlashCommand slashCommand)
            {
                var commandInst = slashCommands.FirstOrDefault(sc => sc.Name == slashCommand.CommandName);
                if (commandInst == null)
                {
                    // Maybe this should be an exception instead?
                    Console.WriteLine("Command not found: {0}", slashCommand.CommandName);
                    return;
                }

                await commandInst.Execute(services, slashCommand);
            }
        }
    }
}