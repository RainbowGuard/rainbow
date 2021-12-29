using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Rainbow.Interactions;

public class RevokeFlagBanBlipHandler : BlipHandler
{
    private readonly DiscordSocketClient _client;

    public RevokeFlagBanBlipHandler(DiscordSocketClient client)
    {
        _client = client;
    }

    public override async Task<BlipResult> HandleBlip(Blip blip, IGuild guild, SocketMessageComponent component)
    {
        var activatingMember = await guild.GetUserAsync(component.User.Id);
        if (!activatingMember.GuildPermissions.BanMembers)
        {
            await component.RespondAsync("You do not have permission to unban users.");
            return new BlipResult { Success = false, ErrorMessage = "User does not have permission to unban users." };
        }

        var user = await _client.GetUserAsync(blip.TargetUserId);
        await component.RespondAsync($"Unbanned user {user.Mention}!");

        return BlipResult.CompletedBlip;
    }
}