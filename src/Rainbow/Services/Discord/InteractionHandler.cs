using Discord.WebSocket;
using Rainbow.Interactions;
using Rainbow.Services.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Rainbow.Services.Discord;

public class InteractionHandler
{
    private readonly IServiceProvider _services;
    private readonly DiscordSocketClient _client;
    private readonly Logger _logger;

    public InteractionHandler(DiscordSocketClient client, Logger logger, IServiceProvider services)
    {
        _client = client;
        _logger = logger;
        _services = services;
    }

    public async Task HandleBlip(SocketMessageComponent component)
    {
        var message = component.Message;
        if (message == null)
        {
            await _logger.Warn(nameof(HandleBlip),
                "Failed to get message from button component interaction");
            return;
        }

        if (message.Author.Id != _client.CurrentUser.Id)
        {
            await _logger.Warn(nameof(HandleBlip),
                "Received an interaction, but this bot is not the author of its attached message");
            return;
        }

        var blipString = component.Data.CustomId;
        if (!Blip.TryParse(blipString, out var blip))
        {
            await _logger.Warn(nameof(HandleBlip),
                $"Failed to read blip string \"{blipString}\"");
            return;
        }

        await _logger.Info(nameof(HandleBlip),
            $"Got blip \"{blip}\"");

        try
        {
            BlipHandler blipHandler = blip switch
            {
                RevokeFlagBanBlip => _services.GetRequiredService<RevokeFlagBanBlipHandler>(),
                _ => throw new InvalidOperationException($"No blip handler exists for blip \"{blip}\"!"),
            };

            await blipHandler.HandleBlip(blip, component);
        }
        catch (Exception e)
        {
            await _logger.Error(nameof(HandleBlip), "Failed to execute blip.", e);
        }
    }
}