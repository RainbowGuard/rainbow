using Rainbow.Core.Commands;
using Rainbow.Core.Entities;

namespace Rainbow.Core.Internal.Actions
{
    internal class DoThingAction : Action<DoThingCommand>
    {
        private readonly IDatabase<FlaggedUser> _repository;

        public DoThingAction(IDatabase<FlaggedUser> repository)
        {
            _repository = repository;
        }

        public override void Execute(DoThingCommand command)
        {
        }
    }
}
