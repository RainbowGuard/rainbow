using Rainbow.Core.Entities;

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
        public void BroadcastFlag(FlaggedUser user);
    }
}
