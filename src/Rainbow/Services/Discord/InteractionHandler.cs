using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Interactions;
using Rainbow.Services.Logging;
using System;
using System.Threading.Tasks;

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
        // I'm not sure if this can actually happen, but I'm checking for it
        // anyways.
        var message = component.Message;
        if (message == null)
        {
            await _logger.Warn(nameof(HandleBlip),
                "Failed to get message from button component interaction");
            return;
        }

        // If we're sent an interaction, but the message the interaction is attached
        // to is not one of ours, something is probably wrong.
        if (message.Author.Id != _client.CurrentUser.Id)
        {
            await _logger.Warn(nameof(HandleBlip),
                "Received an interaction, but this bot is not the author of its attached message");
            return;
        }

        // We only accept interactions in guild channels, because we need to check permissions
        // on everything.
        if (message.Channel is not SocketTextChannel channel)
        {
            await _logger.Warn(nameof(HandleBlip), "Received an interaction, but it was not in a guild channel!");
            return;
        }

        var guild = (IGuild)channel.Guild;

        // We try to parse out the blip string here, and fail if we can't.
        var blipString = component.Data.CustomId;
        if (!Blip.TryParse(blipString, out var blip))
        {
            await _logger.Warn(nameof(HandleBlip),
                $"Failed to read blip string \"{blipString}\"");
            return;
        }

        await _logger.Info(nameof(HandleBlip),
            $"Got blip \"{blip}\"");
        
        // Execute the blip encoded in the blip string.
        try
        {
            BlipHandler blipHandler = blip switch
            {
                RevokeFlagBanBlip => _services.GetRequiredService<RevokeFlagBanBlipHandler>(),
                _ => throw new InvalidOperationException($"No blip handler exists for blip \"{blip}\"!"),
            };

            var result = await blipHandler.HandleBlip(blip, guild, component);
            if (!result.Success)
            {
                await _logger.Warn(nameof(HandleBlip), result.ErrorMessage);
            }
        }
        catch (Exception e)
        {
            await _logger.Error(nameof(HandleBlip), "Failed to execute blip.", e);
        }
    }
}