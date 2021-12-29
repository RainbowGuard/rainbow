using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Rainbow.Interactions;

public abstract class BlipHandler
{
    public abstract Task<BlipResult> HandleBlip(Blip blip, IGuild guild, SocketMessageComponent component);
}