using Discord.WebSocket;
using Rainbow.Interactions;
using Rainbow.Services.Logging;
using System.Threading.Tasks;

namespace Rainbow.Services.Discord;

public class InteractionHandler
{
    private readonly DiscordSocketClient _client;
    private readonly Logger _logger;

    public InteractionHandler(DiscordSocketClient client, Logger logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task BlipHandler(SocketMessageComponent component)
    {
        var message = component.Message;
        if (message == null)
        {
            await _logger.Warn(nameof(BlipHandler),
                "Failed to get message from button component interaction");
            return;
        }

        if (message.Author.Id != _client.CurrentUser.Id)
        {
            await _logger.Warn(nameof(BlipHandler),
                "Received an interaction, but this bot is not the author of its attached message");
            return;
        }

        var blipString = component.Data.CustomId;
        if (!Blip.TryParse(blipString, out var blip))
        {
            await _logger.Warn(nameof(BlipHandler),
                $"Failed to read blip string \"{blipString}\"");
            return;
        }

        await _logger.Info(nameof(BlipHandler),
            $"Got blip \"{blip}\"");

        if (blip is RevokeFlagBanBlip)
        {
            var user = await _client.GetUserAsync(blip.TargetUserId);
            await component.RespondAsync($"Unbanned user {user.Mention}!");
        }
    }
}