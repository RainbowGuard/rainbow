using Discord;
using Discord.Commands;
using Rainbow.Database;
using Rainbow.Interactions;
using Rainbow.Services.Logging;
using System.Threading.Tasks;

namespace Rainbow.Modules;

[RequireContext(ContextType.Guild)]
public class UserFlagModule : ModuleBase<SocketCommandContext>
{
    private readonly RainbowContext _context;
    private readonly Logger _logger;

    public UserFlagModule(RainbowContext context, Logger logger)
    {
        _context = context;
        _logger = logger;
    }

    [Command("botban")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    public async Task BotBanAsync(IUser user, string reason)
    {
        await _logger.Info(nameof(BotBanAsync),
            $"User {user} ({user.Id}) has been flagged as a bot and banned from {Context.Guild.Name} ({Context.Guild.Id})");

        var message = await ReplyAsync(embed: new EmbedBuilder()
            .WithTitle("User flagged and banned!")
            .WithDescription($"{user}: {reason}")
            .WithColor(Color.Magenta)
            .Build());

        await message.ModifyAsync(props => props.Components = new ComponentBuilder()
            .WithButton("Revoke ban", new RevokeFlagBanBlip(user.Id), ButtonStyle.Secondary)
            .Build());
    }
}