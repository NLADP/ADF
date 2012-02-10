namespace Adf.Core.Authorization
{
    /// <summary>
    /// Defines properties and methods that a class implements to login, logout, to get the current user,
    /// to know whether the current user is allowed to perform a specified action, to know whether the 
    /// current user is in the specified role etc.
    /// </summary>
    public interface IAuthorizationProvider
    {
        /// <summary>
        /// Used for login. Returns a value indicating whether the login is successful.
        /// </summary>
        /// <param name="name">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// true if the login is succesful; otherwise, false.
        /// </returns>
        bool Login(string name, string password);
        
        /// <summary>
        /// Gets a value indicating if any user is logged on.
        /// </summary>
        /// <returns>
        /// true if any user is logged on; otherwise, false.
        /// </returns>
        bool IsLoggedOn { get; }
        
        /// <summary>
        /// Logs out the current user.
        /// </summary>
        void Logout();

        
        /// <summary>
        /// Gets the current user if exists. Returns null otherwise.
        /// </summary>
        /// <returns>
        /// The current user if exists. Returns null otherwise.
        /// </returns>
        IUser CurrentUser { get; }

        /// <summary>
        /// Returns a value indicating whether the current user is allowed to performs the 
        /// specified <see cref="IAction"/>.
        /// </summary>
        /// <param name="action">Any action that implements <see cref="IAction"/> (such as ApplicationTask).</param>
        /// <returns>
        /// true if the <see cref="IAction"/> is allowed for the current user; otherwise, false.
        /// </returns>
        bool IsAllowed(IAction action);

        /// <summary>
        /// Returns a value indicating whether the current user is allowed to performs the 
        /// specified <see cref="IAction"/>.
        /// </summary>
        /// <param name="action">Any action that implements <see cref="IAction"/> (such as ApplicationTask).</param>
        /// <returns>
        /// true if the <see cref="IAction"/> is allowed for the current user; otherwise, false.
        /// </returns>
        bool IsAllowed(string subject, IAction action);

        /// <summary>
        /// Returns a value indicating whether the current user belongs to the specified role.
        /// </summary>
        /// <param name="role">The role name to check for.</param>
        /// <returns>
        /// true if the current user is in the specified role, false if not.
        /// </returns>
        bool IsInRole(string role);
    }
}
