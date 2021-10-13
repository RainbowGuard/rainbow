using LibGit2Sharp;
using Rainbow.Entities;
using Rainbow.Junkyard.Exceptions;
using System;

namespace Rainbow.Junkyard.Internal
{
    internal class JunkyardRepo : IRepository<FlaggedUser>, IDisposable
    {
        private readonly Repository _repository;

        public Uri Location { get; }

        public JunkyardRepo(string repositoryUrl, string username, string password)
        {
            _repository = new Repository(repositoryUrl);
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

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}