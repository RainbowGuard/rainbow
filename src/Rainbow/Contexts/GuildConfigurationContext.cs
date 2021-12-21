using Microsoft.EntityFrameworkCore;
using Rainbow.Entities;

namespace Rainbow.Contexts;

public class GuildConfigurationContext : DbContext
{
    public DbSet<GuildConfiguration> GuildConfigurations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=config.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GuildConfiguration>().ToTable(nameof(GuildConfiguration));
    }
}