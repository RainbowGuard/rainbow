using System;
using System.Threading;
using System.Threading.Tasks;
using Rainbow.Core.Entities;

namespace Rainbow.Core
{
    public interface IDatabase<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Sets the specified entity in the database.
        /// </summary>
        /// <param name="entry">The entity to create.</param>
        /// <param name="cancellationToken"></param>
        public Task SetEntity(TEntity entry, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID to check for.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The entity with the provided ID.</returns>
        public Task<TEntity> RetrieveEntity(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the entity with the specified ID from the database, if it exists.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <param name="cancellationToken"></param>
        public Task DeleteEntity(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns if the entity with the provided ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the entity to search for.</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see langword="true" /> if the entity with the provided ID exists in the database, otherwise <see langword="false" />.</returns>
        public Task<bool> HasEntity(string id, CancellationToken cancellationToken = default);
    }
}
