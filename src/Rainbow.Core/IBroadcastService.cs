using Rainbow.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Rainbow.Core
{
    /// <summary>
    /// Handles broadcasting events to clients.
    /// </summary>
    public interface IBroadcastService
    {
        /// <summary>
        /// Broadcasts a user flagged event.
        /// </summary>
        /// <param name="user">The user that was flagged.</param>
        /// <param name="cancellationToken"></param>
        public Task BroadcastFlag(FlaggedUser user, CancellationToken cancellationToken);
    }
}
