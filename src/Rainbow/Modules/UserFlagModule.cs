﻿using Discord;
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
    /// Begins a Rainbow ban sequence. This adds a <see cref="RevokeFlagBanBlip"/> to the response embed,
    /// which we listen for a response for in the <see cref="InteractionHandler"/>.
    /// </summary>
    [Command("rainbowban")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    public async Task RainbowBanAsync(IUser user, string reason)
    {
        // Broadcast a flag to all connected servers
        await _userFlags.FlagUser(Context.Guild, user, reason);

        await _logger.Info(nameof(RainbowBanAsync),
            $"User {user} ({user.Id}) has been flagged as a bot and banned from {Context.Guild.Name} ({Context.Guild.Id})");

        // TODO: Do some servers want to continue to use a separate mechanism to ban users?

        // Create a response message
        await ReplyAsync(
            embed: new EmbedBuilder()
                .WithTitle("User flagged and banned!")
                .WithDescription($"{user}: {reason}")
                .WithColor(Color.Magenta)
                .Build(),
            components: new ComponentBuilder()
                .WithButton("Revoke ban", new RevokeFlagBanBlip(user.Id), ButtonStyle.Danger)
                .Build(),
            messageReference: Context.Message.Reference);
    }

    [Command("rainbowunban")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    public async Task RainbowUnbanAsync(IUser user, string reason)
    {
        // Broadcast an unflag to all connected servers
        await _userFlags.UnflagUser(Context.Guild, user, reason);

        await _logger.Info(nameof(RainbowUnbanAsync),
            $"User {user} ({user.Id}) has been unflagged as a bot and unbanned from {Context.Guild.Name} ({Context.Guild.Id})");

        await ReplyAsync(
            embed: new EmbedBuilder()
                .WithTitle("User unflagged and unbanned!")
                .WithDescription($"{user}: {reason}")
                .WithColor(Color.Magenta)
                .Build(),
            messageReference: Context.Message.Reference);
    }
}