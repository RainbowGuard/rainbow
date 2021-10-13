using Rainbow.Core.Commands;
using Rainbow.Core.Exceptions;

namespace Rainbow.Core.Internal.Actions
{
    internal class DoThingAction
    {
        public void Execute(DoThingCommand command)
        {
            if (command.Permissions == null)
            {
                throw new PermissionsNotFoundException("No permissions were provided with the command.");
            }
        }
    }
}
