using System;
using Rainbow.Core.Entities;

namespace Rainbow.Core
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// The location of the repository.
        /// </summary>
        public Uri Location { get; }

        /// <summary>
        /// Sets the specified entity in the repository.
        /// </summary>
        /// <param name="entry">The entity to create.</param>
        public void SetEntity(TEntity entry);

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID to check for.</param>
        /// <returns>The entity with the provided ID.</returns>
        public TEntity RetrieveEntity(string id);

        /// <summary>
        /// Deletes the entity with the specified ID from the repository, if it exists.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        public void DeleteEntity(string id);

        /// <summary>
        /// Returns if the entity with the provided ID exists in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns><see langword="true" /> if the entity with the provided ID exists in the repository, otherwise <see langword="false" />.</returns>
        public bool HasEntity(TEntity entity);
    }
}
