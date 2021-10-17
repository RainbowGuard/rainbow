using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Core.Internal.Actions
{
    public abstract class Action<TCommand> where TCommand : class
    {
        public abstract Task Execute(TCommand command, CancellationToken cancellationToken);
    }
}