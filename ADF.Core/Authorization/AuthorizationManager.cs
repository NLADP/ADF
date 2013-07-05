using Adf.Core.Objects;

namespace Adf.Core.Authorization
{
    /// <summary>
    /// Represents manager for authorization activities.
    /// Provides methods to login, logout etc.
    /// </summary>
    public static class AuthorizationManager
    {
        private static IAuthorizationProvider _authorizationProvider;
        
        private static readonly object _authorizationProviderLock = new object();

        private static IAuthorizationProvider AuthorizationProvider
        {
            get { lock (_authorizationProviderLock) return _authorizationProvider ?? (_authorizationProvider = ObjectFactory.BuildUp<IAuthorizationProvider>()); }
        }

        /// <summary>
        /// Used for login.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="password">The Password (if required).</param>
        /// <returns>
        /// true if login is succesful; otherwise, false.
        /// </returns>
        public static LoginResult Login(string name, string password)
        {
            return AuthorizationProvider.Login(name, password);
        }

        /// <summary>
        /// Gets a value indicating whether any user is currently logged on.
        /// </summary>
        /// <returns>
        /// true if any user is logged on; otherwise, false.
        /// </returns>
        public static bool IsLoggedOn
        {
            get { return AuthorizationProvider.IsLoggedOn; }
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public static void Logout()
        {
            if (!IsLoggedOn) return;

            AuthorizationProvider.Logout();
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>
        /// The current user. Returns null if no current user is available.
        /// </returns>
        public static IUser CurrentUser
        {
            get { return AuthorizationProvider.CurrentUser; }
        }

        /// <summary>
        /// Checks whether the current user is allowed to performs the specified action.
        /// </summary>
        /// <param name="action">Any action that implements IAction (such as ApplicationTask).</param>
        /// <returns>
        /// true if the action is allowed for this user; otherwise, false.
        /// </returns>
        public static bool IsAllowed(IAction action)
        {
            return AuthorizationProvider.IsAllowed(action); 
        }

        /// <summary>
        /// Checks whether the current user belongs to the specified role.
        /// </summary>
        /// <param name="role">Role name to check for.</param>
        /// <returns>
        /// true if the current user belongs to the specified role; otherwise, false.
        /// </returns>
        public static bool IsInRole(string role)
        {
            return AuthorizationProvider.IsInRole(role);
        }

        /// <summary>
        /// Checks whether the current user is allowed to performs the specified action.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="action">Any action that implements IAction (such as ApplicationTask).</param>
        /// <returns>
        /// true if the action is allowed for this user; otherwise, false.
        /// </returns>
        public static bool IsAllowed(string subject, IAction action) 
        {
            return AuthorizationProvider.IsAllowed(subject, action);
        }

    }
}
