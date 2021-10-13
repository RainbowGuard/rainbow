namespace Rainbow.Core.Permissions
{
    /// <summary>
    /// Permissions policy for staff users of the application (server moderators etc.)
    /// </summary>
    public class TrustedPolicy : PermissionsPolicy
    {
        internal TrustedPolicy()
        {
            CanFlagUsers = true;
            CanListUsers = false;
        }
    }
}