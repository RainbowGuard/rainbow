using Rainbow.Entities;
using System;

namespace Rainbow.Junkyard
{
    internal class JunkyardRepo : IRepository<FlaggedUser>
    {
        public Uri Location { get; }

        public JunkyardRepo(string repositoryUrl)
        {
            Location = new Uri(repositoryUrl);
        }

        public void DeleteEntity(string id)
        {
            throw new NotImplementedException();
        }

        public bool HasEntity(FlaggedUser entity)
        {
            throw new NotImplementedException();
        }

        public FlaggedUser RetrieveEntity(string id)
        {
            throw new NotImplementedException();
        }

        public void SetEntity(FlaggedUser entry)
        {
            throw new NotImplementedException();
        }
    }
}