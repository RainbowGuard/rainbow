using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Rainbow.Services.Flagging;
using System.Threading.Tasks;

namespace Rainbow.Interactions;

public class RevokeFlagBlipHandler : BlipHandler
{
    private readonly DiscordSocketClient _client;
    private readonly UserFlags _userFlags;

    public RevokeFlagBlipHandler(DiscordSocketClient client, UserFlags userFlags)
    {
        _client = client;
        _userFlags = userFlags;
    }

    [RequireUserPermission(GuildPermission.KickMembers)]
    public override async Task HandleBlip(Blip blip, IGuildUser activatingMember, SocketMessageComponent component)
    {
        var targetUser = await _client.GetUserAsync(blip.TargetUserId);
        await _userFlags.UnflagUser(activatingMember.Guild, targetUser, "");
        await component.RespondAsync($"Unflagged user {targetUser.Mention}!");
    }
}