using Rainbow.Core.Commands;

namespace Rainbow.Core.Internal.Actions
{
    internal class FlagUserAction : Action<FlagUserCommand>
    {
        private readonly IBroadcastService _broadcast;

        public FlagUserAction(IBroadcastService broadcast)
        {
            _broadcast = broadcast;
        }

        public override void Execute(FlagUserCommand command)
        {
            // TODO: Track number of flags before doing this
            _broadcast.BroadcastFlag(command.User);
        }
    }
}
