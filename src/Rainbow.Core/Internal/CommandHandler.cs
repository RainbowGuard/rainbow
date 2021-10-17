using Rainbow.Core.Commands;
using Rainbow.Core.Internal.Actions;
using System;

namespace Rainbow.Core.Internal
{
    internal class CommandHandler
    {
        private readonly IServiceProvider _services;

        public CommandHandler(IServiceProvider services)
        {
            _services = services;
        }

        public void Handle(FlagUserCommand command)
        {
            var broadcast = (IBroadcastService)_services.GetService(typeof(IBroadcastService));
            var action = new FlagUserAction(broadcast);
            action.Execute(command);
        }
    }
}
