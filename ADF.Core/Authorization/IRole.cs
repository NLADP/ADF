namespace Adf.Core.Authorization
{
    /// <summary>
    /// Defines property that a value type or class implements to get or set the name of a role.
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// Gets or sets the name of a role.
        /// </summary>
        /// <returns>
        /// The name of a role.
        /// </returns>
        string Name { get; set; }
    }
}
