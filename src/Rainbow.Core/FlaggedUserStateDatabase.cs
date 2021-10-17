using Rainbow.Core.Entities;
using Rainbow.Core.Exceptions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Core
{
    public class FlaggedUserStateDatabase : IDatabase<FlaggedUserState>
    {
        private readonly IDictionary<string, FlaggedUserState> _memoryDb;

        internal FlaggedUserStateDatabase()
        {
            _memoryDb = new ConcurrentDictionary<string, FlaggedUserState>();
        }

        public Task SetEntity(FlaggedUserState entry, CancellationToken cancellationToken)
        {
            _memoryDb[entry.Id] = entry;
            return Task.CompletedTask;
        }

        public Task<FlaggedUserState> RetrieveEntity(string id, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_memoryDb[id]);
            }
            catch (KeyNotFoundException e)
            {
                throw new EntityNotFoundException($"User state not found: {id}", e);
            }
        }

        public Task DeleteEntity(string id, CancellationToken cancellationToken)
        {
            _memoryDb.Remove(id);
            return Task.CompletedTask;
        }

        public Task<bool> HasEntity(string id, CancellationToken cancellationToken)
        {
            return Task.FromResult(_memoryDb.ContainsKey(id));
        }

        /// <summary>
        /// Creates a new flagged user state database.
        /// </summary>
        public static IDatabase<FlaggedUserState> Create()
        {
            return new FlaggedUserStateDatabase();
        }
    }
}