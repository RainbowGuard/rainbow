using Microsoft.EntityFrameworkCore;
using Rainbow.Core;
using Rainbow.Core.Entities;
using Rainbow.Core.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Configuration
{
    public class GuildConfigurationDatabase : IDatabase<GuildConfiguration>
    {
        private readonly IDbContextFactory<GuildConfigurationContext> _factory;

        public GuildConfigurationDatabase(IDbContextFactory<GuildConfigurationContext> factory)
        {
            _factory = factory;
        }

        public async Task SetEntity(GuildConfiguration entry, CancellationToken cancellationToken)
        {
            await using var db = _factory.CreateDbContext();
            db.Add(entry);
            await db.SaveChangesAsync(cancellationToken);
        }

        public async Task<GuildConfiguration> RetrieveEntity(string id, CancellationToken cancellationToken)
        {
            await using var db = _factory.CreateDbContext();
            var entity = await db.Guilds.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Guild configuration not found: {id}");
            }

            return entity;
        }

        public async Task DeleteEntity(string id, CancellationToken cancellationToken)
        {
            await using var db = _factory.CreateDbContext();
            var entity = await RetrieveEntity(id, cancellationToken);
            if (entity != null)
            {
                db.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> HasEntity(string id, CancellationToken cancellationToken)
        {
            await using var db = _factory.CreateDbContext();
            return await db.Guilds.FindAsync(id, cancellationToken) != null;
        }
    }
}