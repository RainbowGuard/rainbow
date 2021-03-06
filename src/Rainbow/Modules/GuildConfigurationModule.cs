using Discord;
using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using Rainbow.Database;
using Rainbow.Entities;
using Rainbow.Services.Logging;
using System.Threading.Tasks;

namespace Rainbow.Modules;

[RequireContext(ContextType.Guild)]
public class GuildConfigurationModule : ModuleBase<SocketCommandContext>
{
    private readonly RainbowContext _context;
    private readonly Logger _logger;

    public GuildConfigurationModule(RainbowContext context, Logger logger)
    {
        _context = context;
        _logger = logger;
    }

    [Command("rbprefix")]
    [RequireUserPermission(GuildPermission.Administrator)]
    public async Task PrefixAsync(string prefix)
    {
        var config = await _context.GuildConfigurations
            .FirstOrDefaultAsync(c => c.Id == Context.Guild.Id);
        if (config == null)
        {
            await _logger.Warn(nameof(PrefixAsync), "No guild configuration was found! Creating a new one.");
            config = new GuildConfiguration { Id = Context.Guild.Id };
            _context.Add(config);
        }

        config.Prefix = prefix;
        await _context.SaveChangesAsync();

        await ReplyAsync($"Rainbow command prefix updated to `{config.Prefix}` for this guild.");
    }
}