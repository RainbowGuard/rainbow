using Rainbow.Core;
using Rainbow.Core.Entities;
using Rainbow.Core.Exceptions;
using System;

namespace Rainbow.Junkyard.Internal
{
    internal class JunkyardRepo : IDatabase<FlaggedUser>
    {
        public Uri Location { get; }

        public JunkyardRepo(string repositoryUrl, string username, string password)
        {
            Location = new Uri(repositoryUrl);
        }

        public void DeleteEntity(string id)
        {
            Sync();
            throw new EntityNotFoundException();
        }

        public bool HasEntity(FlaggedUser entity)
        {
            Sync();
            return false;
        }

        public FlaggedUser RetrieveEntity(string id)
        {
            Sync();
            throw new EntityNotFoundException();
        }

        public void SetEntity(FlaggedUser entry)
        {
            Sync();
            throw new EntityAlreadyExistsException();
        }

        /// <summary>
        /// Synchronizes the local and remote repository states.
        /// </summary>
        private void Sync()
        {
        }
    }
}