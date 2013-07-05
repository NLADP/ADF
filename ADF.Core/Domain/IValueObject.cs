namespace Adf.Core.Domain
{
	/// <summary>
    /// Defines property, methods that a class implements to get the value of a value object, to convert 
    /// a value object to a string, to know whether a value object is empty.
	/// </summary>
	public interface IValueObject
	{
        /// <summary>
        /// Converts the value object to a string and returns the generated string.
        /// </summary>
        /// <returns>
        /// The generated string.
        /// </returns>
        string ToString();

        /// <summary>
        /// Gets a value indicating whether the value object is empty.
        /// </summary>
        /// <returns>
        /// true if the value object is empty; otherwise, false.
        /// </returns>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets the value of the value object.
        /// </summary>
        /// <returns>
        /// The value of the value object.
        /// </returns>
        object Value { get; }
	}
}
