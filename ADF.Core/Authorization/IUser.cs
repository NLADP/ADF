namespace Adf.Core.Authorization
{
    /// <summary>
    /// Defines property and methods that a class implements to get the title of a user, to check whether
    /// the specified action is allowed for a user, to check whether a user is in the role.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets the title of the user.
        /// This property is used to display default information on the user.
        /// This allows for implementation of IUser using DomainObjects. DomainObject all have a 
        /// Title property.
        /// </summary>
        /// <returns>
        /// The title of the user.
        /// </returns>
        string Title { get; }
        
        /// <summary>
        /// Checks and returns a value indicating whether the user is allowed to performs the specified <see cref="IAction"/>.
        /// </summary>
        /// <param name="action">Any action that implements <see cref="IAction"/> (such as ApplicationTask).</param>
        /// <returns>
        /// true if the <see cref="IAction"/> is allowed for this user; otherwise, false.
        /// </returns>
        bool IsAllowed(IAction action);
        
        /// <summary>
        /// Checks and returns a value indicating whether the user belongs to the specified role.
        /// </summary>
        /// <param name="role">The role name to check for.</param>
        /// <returns>
        /// true if the user belongs to the specified role; otherwise, false.
        /// </returns>
        bool IsInRole(string role);
    }
}
