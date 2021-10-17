using Rainbow.Core.Commands;
using Rainbow.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Core.Internal.Actions
{
    internal class FlagUserAction : Action<FlagUserCommand>
    {
        private readonly IBroadcastService _broadcast;
        private readonly IDatabase<FlaggedUserState> _db;

        public FlagUserAction(IBroadcastService broadcast, IDatabase<FlaggedUserState> db)
        {
            _broadcast = broadcast;
            _db = db;
        }

        public override async Task Execute(FlagUserCommand command, CancellationToken cancellationToken)
        {
            if (await _db.HasEntity(command.User.Id, cancellationToken))
            {
                var flaggedUser = await _db.RetrieveEntity(command.User.Id, cancellationToken);

                if (flaggedUser.FlagCount >= 2)
                {
                    await _broadcast.BroadcastFlag(command.User, cancellationToken);
                }
                else
                {
                    flaggedUser.FlagCount++;
                    await _db.SetEntity(flaggedUser, cancellationToken);
                }
            }
            else
            {
                await _db.SetEntity(new FlaggedUserState
                {
                    Id = command.User.Id,
                    FlagCount = 1,
                }, cancellationToken);
            }
        }
    }
}
