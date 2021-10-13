namespace Rainbow.Core.Permissions
{
    public abstract class PermissionsPolicy
    {
        /// <summary>
        /// Whether or not agents with this policy can flag users.
        /// </summary>
        public bool CanFlagUsers { get; protected init; }

        /// <summary>
        /// Whether or not agents with this policy can add users to the scam/bot account
        /// list without flagging them.
        /// </summary>
        public bool CanListUsers { get; protected init; }

        internal PermissionsPolicy() { }

        public static readonly PermissionsPolicy Default = new DefaultPolicy();
        public static readonly PermissionsPolicy Trusted = new TrustedPolicy();
        public static readonly PermissionsPolicy Administrator = new AdministratorPolicy();
    }
}