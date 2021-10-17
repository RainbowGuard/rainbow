using Microsoft.EntityFrameworkCore;
using Rainbow.Core.Entities;

namespace Rainbow.Configuration
{
    public class GuildConfigurationContext : DbContext
    {
        public DbSet<GuildConfiguration> Guilds { get; set; }
        
        public GuildConfigurationContext(DbContextOptions<GuildConfigurationContext> options) : base(options) { }
    }
}
