using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Interactions;
using Rainbow.Services.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;

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
        if (message.Channel is not SocketTextChannel { Guild: IGuild guild } channel)
        {
            await _logger.Warn(nameof(HandleBlip), "Received an interaction, but it was not in a guild channel!");
            return;
        }

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
        
        try
        {
            // Get the handler for the provided blip.
            BlipHandler blipHandler = blip switch
            {
                RevokeFlagBlip => _services.GetRequiredService<RevokeFlagBlipHandler>(),
                _ => throw new InvalidOperationException($"No blip handler exists for blip type \"{blip.Type}\"!"),
            };

            // Get the guild member executing this blip.
            var activatingMember = await guild.GetUserAsync(component.User.Id);
            if (activatingMember == null)
            {
                await _logger.Warn(nameof(HandleBlip), $"Failed to get the guild member entry for {component.User} in {guild.Name}");
                return;
            }

            // If the blip requires a certain permissions level, check that this guild member
            // is permitted to execute this blip under the current context.
            var handleBlipMethod = blipHandler.GetType().GetMethod(nameof(BlipHandler.HandleBlip));
            var userPermissionsAttribute = handleBlipMethod?.GetCustomAttribute<RequireUserPermissionAttribute>();
            if (userPermissionsAttribute != null)
            {
                if (userPermissionsAttribute.GuildPermission.HasValue && !activatingMember.GuildPermissions.Has(userPermissionsAttribute.GuildPermission.Value))
                {
                    await component.RespondAsync("You do not have permission to perform this action.");
                    await _logger.Warn(nameof(HandleBlip),
                        $"{component.User} does not have permission to execute blips of type \"{blip.Type}\" in {guild.Name}");
                    return;
                }

                if (userPermissionsAttribute.ChannelPermission.HasValue && !activatingMember.GetPermissions(channel).Has(userPermissionsAttribute.ChannelPermission.Value))
                {
                    await component.RespondAsync("You do not have permission to perform this action.");
                    await _logger.Warn(nameof(HandleBlip),
                        $"{component.User} does not have permission to execute blips of type \"{blip.Type}\" in channel {channel.Name} ({guild.Name})");
                    return;
                }
            }

            // Execute the blip encoded in the blip string.
            await blipHandler.HandleBlip(blip, activatingMember, component);
        }
        catch (Exception e)
        {
            await _logger.Error(nameof(HandleBlip), "Failed to execute blip.", e);
        }
    }
}