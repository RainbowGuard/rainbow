using Rainbow.Core.Exceptions;
using Rainbow.Core.Internal;
using System;
using System.Linq;

namespace Rainbow.Core
{
    public class Core
    {
        private readonly CommandHandler _commandHandler;

        public Core(IServiceProvider services)
        {
            _commandHandler = new CommandHandler(services);
        }

        public void HandleCommand<TCommand>(TCommand command)
        {
            var handler = _commandHandler.GetType().GetMethods().FirstOrDefault(m =>
                m.GetParameters().FirstOrDefault()?.ParameterType.IsAssignableFrom(command.GetType()) == true);
            if (handler == null)
            {
                throw new CommandNotFoundException(command.GetType().Name);
            }

            handler.Invoke(command, null);
        }
    }
}
