namespace Rainbow.Core.Internal.Actions
{
    public abstract class Action<TCommand> where TCommand : class
    {
        public abstract void Execute(TCommand command);
    }
}