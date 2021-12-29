using Discord;
using Microsoft.EntityFrameworkCore;
using Rainbow.Database;
using Rainbow.Entities;
using Rainbow.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rainbow.Services.Flagging;

public class UserFlags
{
    private readonly RainbowContext _context;
    private readonly Logger _logger;

    public Func<IGuild, IUser, string, Task> OnUserFlag { get; set; }

    public Func<IGuild, IUser, string, Task> OnUserUnflag { get; set; }

    public UserFlags(RainbowContext context, Logger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task FlagUser(IGuild guild, IUser user, string flagReason)
    {
        // Get or create the flagged user row
        var flaggedUser = await _context.FlaggedUsers.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (flaggedUser == null)
        {
            flaggedUser = new FlaggedUser { Id = user.Id, FlagGuilds = new List<Guild>(), FlagReason = "" };
            _context.FlaggedUsers.Add(flaggedUser);
        }

        // Add to the flag reason regardless of if the guild has already flagged the user or not
        if (!string.IsNullOrEmpty(flagReason))
        {
            if (flaggedUser.FlagReason == string.Empty)
            {
                flaggedUser.FlagReason = flagReason;
            }
            else
            {
                flaggedUser.FlagReason += $" | {flagReason}";
            }
        }

        // Check if the user has already been flagged by this guild
        if (flaggedUser.FlagGuilds.Any(g => g.Id == guild.Id))
        {
            await _logger.Info(nameof(FlagUser), $"User {user} ({user.Id}) has already been flagged by guild {guild.Name} ({guild.Id}), ignoring flag request...");
            return;
        }

        // Get or create a guild row
        var dbGuild = await _context.Guilds.FirstOrDefaultAsync(g => g.Id == guild.Id);
        if (dbGuild == null)
        {
            dbGuild = new Guild { Id = guild.Id };
            _context.Guilds.Add(dbGuild);
        }

        // Add the guild to the user
        flaggedUser.FlagGuilds.Add(dbGuild);

        await _context.SaveChangesAsync();

        await _logger.Info(nameof(FlagUser), $"User {user} ({user.Id}) has been flagged by guild {guild.Id} ({guild.Id})");
        OnUserFlag?.Invoke(guild, user, flagReason);
    }

    public async Task UnflagUser(IGuild guild, IUser user, string unflagReason)
    {
        // Get or create the flagged user row
        var flaggedUser = await _context.FlaggedUsers.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (flaggedUser == null)
        {
            return;
        }

        // Check if the user has been flagged by this guild
        if (flaggedUser.FlagGuilds.All(g => g.Id != guild.Id))
        {
            await _logger.Info(nameof(UnflagUser), $"User {user} ({user.Id}) has not been flagged by guild {guild.Name} ({guild.Id}), ignoring unflag request...");
            return;
        }

        // Get or create a guild row
        var dbGuild = await _context.Guilds.FirstOrDefaultAsync(g => g.Id == guild.Id);
        if (dbGuild == null)
        {
            dbGuild = new Guild { Id = guild.Id };
            _context.Guilds.Add(dbGuild);
        }

        // Remove the guild to the user
        flaggedUser.FlagGuilds.Remove(dbGuild);

        // If the user has no guilds flagging them, remove them from the database
        if (!flaggedUser.FlagGuilds.Any())
        {
            _context.FlaggedUsers.Remove(flaggedUser);
        }

        await _context.SaveChangesAsync();

        await _logger.Info(nameof(UnflagUser), $"User {user} ({user.Id}) has been unflagged by guild {guild.Name} ({guild.Id})");
        OnUserUnflag?.Invoke(guild, user, unflagReason);
    }
}