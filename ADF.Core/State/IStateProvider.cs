namespace Adf.Core.State
{
	/// <summary>
    /// Defines a state object. State objects are used by the <see cref="StateManager"/>.
    /// Defines properties and methods that a value type or class implements to get, set, remove a value 
    /// from the state corresponding to a specified key etc.
	/// </summary>
	public interface IStateProvider
	{
        /// <summary>
        /// Returns a value indicating whether a value exists in the state corresponding to the 
        /// specified key object.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
		bool Has(object o);

        /// <summary>
        /// Returns a value indicating whether a value exists in the state corresponding to the 
        /// specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
		bool Has(string key);

        /// <summary>
        /// Returns a value indicating whether a value exists in the state corresponding to the 
        /// specified key (combination of the specified object and string).
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <param name="key">The string part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
		bool Has(object o, string key);
		
        /// <summary>
        /// Gets or sets the value in the state corresponding to the specified key object.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key object is 
        /// returned.
        /// </returns>
		object this[object o] { get; set; }

        /// <summary>
        /// Gets or sets the value in the state corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key is 
        /// returned.
        /// </returns>
		object this[string key] { get; set; }

        /// <summary>
        /// Gets or sets the value in the state corresponding to the specified key (combination of the specified object and string).
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value is get or set.</param>
        /// <param name="key">The string part of the key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the 
        /// key (combination of the specified object and string) is returned.
        /// </returns>
		object this[object o, string key] { get; set; }

        /// <summary>
        /// Removes the value from the state corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is to be removed.</param>
		void Remove(string key);

        /// <summary>
        /// Removes the value from the state corresponding to the specified key object.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is to be removed.</param>
		void Remove(object o);

        /// <summary>
        /// Removes all values from the state
        /// </summary>
	    void Clear();
	}
}


