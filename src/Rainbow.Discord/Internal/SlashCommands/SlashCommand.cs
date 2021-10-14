using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Rainbow.Discord.Internal.SlashCommands
{
    internal abstract class SlashCommand
    {
        public string Name { get; protected init; }

        public abstract Task Execute(ServiceProvider services, SocketSlashCommand slashCommand);
    }
}