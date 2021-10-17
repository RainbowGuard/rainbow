using Rainbow.Core.Commands;
using Rainbow.Core.Entities;
using Rainbow.Core.Internal.Actions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Core.Internal
{
    internal class CommandHandler
    {
        private readonly IServiceProvider _services;

        public CommandHandler(IServiceProvider services)
        {
            _services = services;
        }

        public Task Handle(FlagUserCommand command, CancellationToken cancellationToken)
        {
            var broadcast = (IBroadcastService)_services.GetService(typeof(IBroadcastService));
            var database = (IDatabase<FlaggedUserState>)_services.GetService(typeof(IDatabase<FlaggedUserState>));
            var action = new FlagUserAction(broadcast, database);
            return action.Execute(command, cancellationToken);
        }
    }
}
