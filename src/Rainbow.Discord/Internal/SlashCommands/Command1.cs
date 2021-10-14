using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Rainbow.Discord.Internal.SlashCommands
{
    internal class Command1 : SlashCommand
    {
        public Command1()
        {
            Name = SlashCommandNames.Command1;
        }

        public override Task Execute(ServiceProvider services, SocketSlashCommand slashCommand)
        {
            return Task.CompletedTask;
        }
    }
}
