using System.Threading.Tasks;
using Discord.WebSocket;
using Rainbow.Services.Logging;

namespace Rainbow.Interactions;

public class RevokeFlagBanBlipHandler : BlipHandler
{
    private readonly DiscordSocketClient _client;
    private readonly Logger _logger;

    public RevokeFlagBanBlipHandler(DiscordSocketClient client, Logger logger)
    {
        _client = client;
        _logger = logger;
    }

    public override async Task HandleBlip(Blip blip, SocketMessageComponent component)
    {
        var user = await _client.GetUserAsync(blip.TargetUserId);
        await component.RespondAsync($"Unbanned user {user.Mention}!");
    }
}