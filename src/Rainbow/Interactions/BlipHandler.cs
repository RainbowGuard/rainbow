using System.Threading.Tasks;
using Discord.WebSocket;

namespace Rainbow.Interactions;

public abstract class BlipHandler
{
    public abstract Task HandleBlip(Blip blip, SocketMessageComponent component);
}