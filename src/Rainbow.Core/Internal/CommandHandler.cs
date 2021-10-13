using Rainbow.Core.Commands;
using Rainbow.Core.Entities;
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

        public void Handle(DoThingCommand command)
        {
            var repository = (IRepository<FlaggedUser>)_services.GetService(typeof(IRepository<FlaggedUser>));
            var action = new DoThingAction(repository);
            action.Execute(command);
        }
    }
}
