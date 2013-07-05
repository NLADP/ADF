namespace Adf.Core.Identity
{
	/// <summary>
    /// Defines properties and methods that a value type or class implements to get empty ID, 
    /// create new ID, check two IDs.
	/// </summary>
	public interface IIdProvider 
	{
        /// <summary>
        /// Creates and returns a new <see cref="ID"/> with the specified parameters.
        /// </summary>
        /// <param name="p">The parameters to use.</param>
        /// <returns>
        /// The newly created <see cref="ID"/>.
        /// </returns>
		ID NewId(params object[] p);
	}
}
