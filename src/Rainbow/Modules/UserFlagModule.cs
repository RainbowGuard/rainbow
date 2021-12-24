using Discord;
using Discord.Commands;
using Rainbow.Database;
using Rainbow.Interactions;
using Rainbow.Services.Logging;
using System.Threading.Tasks;
using Rainbow.Services.Flagging;

namespace Rainbow.Modules;

[RequireContext(ContextType.Guild)]
public class UserFlagModule : ModuleBase<SocketCommandContext>
{
    private readonly Logger _logger;
    private readonly UserFlags _userFlags;

    public UserFlagModule(Logger logger, UserFlags userFlags)
    {
        _logger = logger;
        _userFlags = userFlags;
    }

    [Command("botban")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    public async Task BotBanAsync(IUser user, string reason)
    {
        await _logger.Info(nameof(BotBanAsync),
            $"User {user} ({user.Id}) has been flagged as a bot and banned from {Context.Guild.Name} ({Context.Guild.Id})");

        await _userFlags.FlagUser(Context.Guild, user, reason);

        var message = await ReplyAsync(embed: new EmbedBuilder()
            .WithTitle("User flagged and banned!")
            .WithDescription($"{user}: {reason}")
            .WithColor(Color.Magenta)
            .Build());

        await message.ModifyAsync(props => props.Components = new ComponentBuilder()
            .WithButton("Revoke ban", new RevokeFlagBanBlip(user.Id), ButtonStyle.Danger)
            .Build());
    }

    [Command("botunban")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    public async Task BotUnbanAsync(IUser user, string reason)
    {
        await _logger.Info(nameof(BotUnbanAsync),
            $"User {user} ({user.Id}) has been unflagged as a bot and unbanned from {Context.Guild.Name} ({Context.Guild.Id})");

        await _userFlags.UnflagUser(Context.Guild, user, reason);

        await ReplyAsync(embed: new EmbedBuilder()
            .WithTitle("User unflagged and unbanned!")
            .WithDescription($"{user}: {reason}")
            .WithColor(Color.Magenta)
            .Build());
    }
}