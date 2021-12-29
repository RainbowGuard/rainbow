using Discord;
using Discord.Commands;
using Rainbow.Interactions;
using Rainbow.Services.Discord;
using Rainbow.Services.Flagging;
using Rainbow.Services.Logging;
using System.Threading.Tasks;

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

    /// <summary>
    /// Begins a Rainbow flag sequence. This adds a <see cref="RevokeFlagBlip"/> to the response embed,
    /// which we listen for a response for in the <see cref="InteractionHandler"/>.
    /// </summary>
    [Command("rbflag")]
    [RequireUserPermission(GuildPermission.KickMembers)]
    public async Task RainbowFlagAsync(IUser user, string reason)
    {
        // Broadcast a flag event to all connected servers
        await _userFlags.FlagUser(Context.Guild, user, reason);

        await _logger.Info(nameof(RainbowFlagAsync),
            $"User {user} ({user.Id}) has been flagged in {Context.Guild.Name} ({Context.Guild.Id})");

        // TODO: Do some servers want to continue to use a separate mechanism to ban users?
        // I should ask if people want the flag and ban functions to be combined, or if
        // people want to use their own things in addition to this (e.g. for custom ban
        // messages). I'm leaving this at flag-only for now, but it should be simple enough
        // to extend this to a ban/unban as well. Maybe I can add a setting for this in the
        // per-guild configuration. If I add that, I need to remember to update the user
        // permissions for this command.

        // Create a response message
        await ReplyAsync(
            embed: new EmbedBuilder()
                .WithTitle("User flagged!")
                .WithDescription($"{user}: {reason}")
                .WithColor(Color.Magenta)
                .Build(),
            components: new ComponentBuilder()
                .WithButton("Unflag", new RevokeFlagBlip(user.Id), ButtonStyle.Danger)
                .Build(),
            messageReference: Context.Message.Reference);
    }

    [Command("rbunflag")]
    [RequireUserPermission(GuildPermission.KickMembers)]
    public async Task RainbowUnflagAsync(IUser user, string reason)
    {
        // Broadcast an unflag event to all connected servers
        await _userFlags.UnflagUser(Context.Guild, user, reason);

        await _logger.Info(nameof(RainbowUnflagAsync),
            $"User {user} ({user.Id}) has been unflagged in {Context.Guild.Name} ({Context.Guild.Id})");

        await ReplyAsync(
            embed: new EmbedBuilder()
                .WithTitle("User unflagged!")
                .WithDescription($"{user}: {reason}")
                .WithColor(Color.Magenta)
                .Build(),
            messageReference: Context.Message.Reference);
    }
}