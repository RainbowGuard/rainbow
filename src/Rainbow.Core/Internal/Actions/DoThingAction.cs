using Rainbow.Core.Commands;
using Rainbow.Core.Entities;

namespace Rainbow.Core.Internal.Actions
{
    internal class DoThingAction : Action<DoThingCommand>
    {
        private readonly IRepository<FlaggedUser> _repository;

        public DoThingAction(IRepository<FlaggedUser> repository)
        {
            _repository = repository;
        }

        public override void Execute(DoThingCommand command)
        {
        }
    }
}
