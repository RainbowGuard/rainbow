using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Rainbow.Interactions;

public class RevokeFlagBlipHandler : BlipHandler
{
    private readonly DiscordSocketClient _client;

    public RevokeFlagBlipHandler(DiscordSocketClient client)
    {
        _client = client;
    }

    [RequireUserPermission(GuildPermission.KickMembers)]
    public override async Task HandleBlip(Blip blip, IGuildUser member, SocketMessageComponent component)
    {
        var user = await _client.GetUserAsync(blip.TargetUserId);
        await component.RespondAsync($"Unbanned user {user.Mention}!");
    }
}