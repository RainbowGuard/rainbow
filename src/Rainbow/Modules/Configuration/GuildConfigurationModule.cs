using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Rainbow.Modules.Configuration;

[RequireContext(ContextType.Guild)]
public class GuildConfigurationModule : ModuleBase<SocketCommandContext>
{
    [Command("rbprefix")]
    [RequireUserPermission(GuildPermission.Administrator)]
    public async Task PrefixAsync(char prefix)
    {
        await ReplyAsync("Rainbow command prefix updated for this guild.");
    }
}