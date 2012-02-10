namespace Adf.Core.Authorization
{
    /// <summary>
    /// Defines property that a value type or class implements to get its name.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>
        /// The name.
        /// </returns>
        string Name { get; }
    }
}
