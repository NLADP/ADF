using Adf.Core.Authorization;

namespace Adf.Base.Authorization
{
    /// <summary>
    /// Represents an Authorization service which will not allow any user to login to the system.
    /// </summary>
    public class AllAuthorizationProvider : IAuthorizationProvider
    {
        #region IAuthorizationProvider Members

        /// <summary>
        /// Tries to log in using user credential.
        /// </summary>
        /// <param name="name">Login name.</param>
        /// <param name="password">Password (if required).</param>
        /// <returns>It always returns false. It indicates login is always impossible.</returns>
        public bool Login(string name, string password)
        {
            return false;
        }

        /// <summary>
        /// Gets whether any user is logged on or not. 
        /// </summary>
        /// <returns>It always returns false.</returns>
        public bool IsLoggedOn
        {
            get { return false; }
        }

        /// <summary>
        /// Logs out the current user. Here no action is taken.
        /// </summary>
        public void Logout()
        {
            // Do nothing.
        }

        /// <summary>
        /// Gets the current user if exists.
        /// </summary>
        /// <returns>It always returns null.</returns>
        public IUser CurrentUser
        {
            get { return null; }
        }

        /// <summary>
        /// Checks whether the current user is allowed to performs the IAction.
        /// </summary>
        /// <param name="action">Any action that implements IAction (such as ApplicationTask).</param>
        /// <returns>True if the action is allowed for this user, false otherwise. Here it always 
        /// returns true.</returns>
        public bool IsAllowed(IAction action)
        {
            return true;
        }

        /// <summary>
        /// Checks whether the current user is allowed to performs the IAction.
        /// </summary>
        /// <param name="action">Any action that implements IAction (such as ApplicationTask).</param>
        /// <returns>True if the action is allowed for this user, false otherwise. Here it always 
        /// returns true.</returns>
        public bool IsAllowed(string subject, IAction action) 
        {
            return true;
        }

        /// <summary>
        /// Checks whether the current user belongs to a certain role.
        /// </summary>
        /// <param name="role">Role name to check for.</param>
        /// <returns>True if user is in this role, false if not. Here it always returns true.</returns>
        public bool IsInRole(string role)
        {
            return true;
        }

        #endregion
    }
}