using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Rainbow.Interactions;

public abstract class BlipHandler
{
    public abstract Task HandleBlip(Blip blip, IGuildUser activatingMember, SocketMessageComponent component);
}