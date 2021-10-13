namespace Rainbow.Core.Permissions
{
    /// <summary>
    /// Permissions policy for the application administrator.
    /// </summary>
    public class AdministratorPolicy : PermissionsPolicy
    {
        internal AdministratorPolicy()
        {
            CanFlagUsers = true;
            CanListUsers = true;
        }
    }
}