using Discord.WebSocket;
using Rainbow.Core;
using Rainbow.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Discord.Internal
{
    internal class DiscordBroadcastService : IBroadcastService
    {
        private readonly DiscordSocketClient _client;

        public DiscordBroadcastService(DiscordSocketClient client)
        {
            _client = client;
        }

        public Task BroadcastFlag(FlaggedUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
