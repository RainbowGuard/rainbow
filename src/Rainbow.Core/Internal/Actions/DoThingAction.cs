using Rainbow.Core.Commands;
using Rainbow.Core.Entities;

namespace Rainbow.Core.Internal.Actions
{
    internal class DoThingAction
    {
        private readonly IRepository<FlaggedUser> _repository;

        public DoThingAction(IRepository<FlaggedUser> repository)
        {
            _repository = repository;
        }

        public void Execute(DoThingCommand command)
        {
        }
    }
}
