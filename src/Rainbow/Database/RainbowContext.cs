using System;
using Microsoft.EntityFrameworkCore;
using Rainbow.Entities;

namespace Rainbow.Database;

public class RainbowContext : DbContext
{
    public DbSet<GuildConfiguration> GuildConfigurations { get; set; }
    public DbSet<FlaggedUser> FlaggedUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySql("Database=rainbow;Server=localhost;Port=3306;UID=rainbow;PWD=rainbow;",
            new MariaDbServerVersion(new Version(10, 5, 9)));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GuildConfiguration>().ToTable(nameof(GuildConfiguration));
        modelBuilder.Entity<FlaggedUser>().ToTable(nameof(FlaggedUser));
    }
}