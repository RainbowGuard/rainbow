using Rainbow.Core.Exceptions;
using Rainbow.Core.Internal;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Core
{
    public class CoreService
    {
        private readonly CommandHandler _commandHandler;

        public CoreService(IServiceProvider services)
        {
            _commandHandler = new CommandHandler(services);
        }

        public Task HandleCommand<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        {
            var handler = _commandHandler.GetType().GetMethods().FirstOrDefault(m =>
            {
                var parameterType = m.GetParameters().FirstOrDefault()?.ParameterType;
                return parameterType != null &&
                       parameterType.IsInstanceOfType(command);
            });

            if (handler == null)
            {
                throw new CommandNotFoundException(command.GetType().Name);
            }

            return (Task)handler.Invoke(command, new object[] { cancellationToken });
        }
    }
}
