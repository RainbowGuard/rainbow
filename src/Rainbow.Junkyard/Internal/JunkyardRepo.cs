using System;
using LibGit2Sharp;
using Rainbow.Entities;

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
            throw new NotImplementedException();
        }

        public bool HasEntity(FlaggedUser entity)
        {
            Sync();
            throw new NotImplementedException();
        }

        public FlaggedUser RetrieveEntity(string id)
        {
            Sync();
            throw new NotImplementedException();
        }

        public void SetEntity(FlaggedUser entry)
        {
            Sync();
            throw new NotImplementedException();
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