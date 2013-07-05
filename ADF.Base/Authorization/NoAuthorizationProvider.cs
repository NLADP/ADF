using Adf.Core.Authorization;
using Adf.Core.Objects;

namespace Adf.Base.Authorization
{
    /// <summary>    
    /// Represents an authorization service which allows any user to login to the system. 
    /// </summary>
    public class NoAuthorizationProvider : IAuthorizationProvider
    {
        /// <summary>
        /// Tries to log in using user credential.
        /// </summary>
        /// <param name="name">Login name</param>
        /// <param name="password">Password (if required)</param>
        /// <returns>It always returns true. It indicates login is always possible.</returns>
        public LoginResult Login(string name, string password)
        {
            return LoginResult.Success;
        }

        /// <summary>
        /// Checks if any user is logged on.
        /// </summary>
        /// <returns>It always returns true.</returns>
        public bool IsLoggedOn
        {
            get { return true; }
        }

        /// <summary>
        /// Logs out the current user. Here no action is taken.
        /// </summary>
        public void Logout()
        {
            // Do nothing.
        }

        /// <summary>
        /// Returns the current user if exists. Here it always returns null.
        /// </summary>
        /// <returns>It always returns null.</returns>
        public IUser CurrentUser
        {
            get { return ObjectFactory.BuildUp<IUser>("DefaultUser"); }
        }

        /// <summary>
        /// Checks whether the current user is allowed to performs the IAction.
        /// </summary>
        /// <param name="action">Any action that implements IAction (such as ApplicationTask).</param>
        /// <returns>True if the action is allowed for this user, false otherwise. Here it always 
        /// returns false.</returns>
        public bool IsAllowed(IAction action)
        {
            return false;
        }

        /// <summary>
        /// Checks whether the current user is allowed to performs the IAction.
        /// </summary>
        /// <param name="subject">The subject for which authentication is requested.</param>
        /// <param name="action">Any action that implements IAction (such as ApplicationTask).</param>
        /// <returns>True if the action is allowed for this user, false otherwise. Here it always 
        /// returns false.</returns>
        public bool IsAllowed(string subject, IAction action) 
        {
            return false;
        }

        #region IAuthorizationProvider Members

        /// <summary>
        /// Check whether the current user belongs to a certain role.
        /// </summary>
        /// <param name="role">Role name to check for.</param>
        /// <returns>True if user is in this role, false if not. Here it always returns false.</returns>
        public bool IsInRole(string role)
        {
           return false;
        }

        #endregion
    }
}
