using System;
using Microsoft.EntityFrameworkCore;
using Rainbow.Entities;

namespace Rainbow.Contexts;

public class RainbowContext : DbContext
{
    public DbSet<GuildConfiguration> GuildConfigurations { get; set; }
    public DbSet<FlaggedUser> FlaggedUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySql(Db.ConnectionString, Db.Version);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GuildConfiguration>().ToTable(nameof(GuildConfiguration));
        modelBuilder.Entity<FlaggedUser>().ToTable(nameof(FlaggedUser));
    }
}