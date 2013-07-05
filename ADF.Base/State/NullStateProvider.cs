using System;
using Adf.Core.State;

namespace Adf.Base.State
{
	/// <summary>
	/// Represents special type of state that is empty.
    /// Provides properties and methods to get, set, remove a value from the empty state 
    /// corresponding to a specified key etc.
	/// </summary>
	public class NullStateProvider : IStateProvider
	{
	    private static NullStateProvider nullstate = new NullStateProvider();

        /// <summary>
        /// Returns the <see cref="NullStateProvider"/>.
        /// </summary>
	    public static NullStateProvider Null
	    {
			get { return nullstate; }
	    }

        /// <summary>
        /// Returns a value indicating that no value exists in the empty state corresponding to the specified key object.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// Always returns false.
        /// </returns>
	    public bool Has(object o)
		{
			return false;
		}

        /// <summary>
        /// Returns a value indicating that no value exists in the empty state corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// Always returns false.
        /// </returns>
        public bool Has(string key)
		{
			return false;
		}

        /// <summary>
        /// Returns a value indicating that no value exists in the empty state corresponding to the 
        /// specified key (combination of the specified object and string).
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <param name="key">The string part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// Always returns false.
        /// </returns>
		public bool Has(object o, string key)
		{
			return false;
		}

        /// <summary>
        /// Gets or sets the value corresponding to the specified key object in the empty state.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key object is returned.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// The property is yet to be implemented.
        /// </exception>
		public object this[object o]
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

        /// <summary>
        /// Gets or sets the value corresponding to the specified key in the empty state.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key is returned.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// The property is yet to be implemented.
        /// </exception>
		public object this[string key]
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

        /// <summary>
        /// Gets or sets the value corresponding to the specified key (combination of the specified object and string) 
        /// in the empty state.
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value is get or set.</param>
        /// <param name="key">The string part of the key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the 
        /// key (combination of the specified object and string) is returned.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// The property is yet to be implemented.
        /// </exception>
		public object this[object o, string key]
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

        /// <summary>
        /// Removes the value corresponding to the specified key from the empty state.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is to be removed.</param>
        /// <exception cref="System.NotImplementedException">
        /// The method is yet to be implemented.
        /// </exception>
		public void Remove(string key)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Removes the value corresponding to the specified key object from the empty state.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is to be removed.</param>
        /// <exception cref="System.NotImplementedException">
        /// The method is yet to be implemented.
        /// </exception>
		public void Remove(object o)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Removes all values from the state
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }
	}
}
