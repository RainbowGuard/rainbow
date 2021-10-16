using Discord.WebSocket;
using Rainbow.Core;
using Rainbow.Core.Entities;

namespace Rainbow.Discord.Internal
{
    internal class DiscordBroadcastService : IBroadcastService
    {
        private readonly DiscordSocketClient _client;

        public DiscordBroadcastService(DiscordSocketClient client)
        {
            _client = client;
        }

        public void BroadcastFlag(FlaggedUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}
