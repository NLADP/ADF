using System.Web;
using Adf.Core.State;

namespace Adf.Web.State
{
	/// <summary>
    /// Represents a web state provider. This is used by the Adf.Core.StateManager.
    /// Provides properties and methods to get, set, remove a value from Session 
    /// corresponding to a specified key etc.
	/// </summary>
	public class WebStateProvider : IStateProvider
	{
		/// <summary>
        /// Gets or sets the value in the Session corresponding to the specified key.
		/// </summary>
        /// <param name="key">The key object for which the corresponding value esists or not, is to check.</param>
        /// <returns>
        /// The value in the Session corresponding to the specified key.
        /// </returns>
		public object this[string key]
		{
            get { return HttpContext.Current == null || HttpContext.Current.Session == null ? null : HttpContext.Current.Session[key]; }
            set { if (HttpContext.Current != null && HttpContext.Current.Session != null) HttpContext.Current.Session[key] = value; }
		}

		/// <summary>
        /// Returns a value indicating whether a value exists in the Session corresponding 
        /// to the specified key object.
		/// </summary>
        /// <param name="o">The key object for which the corresponding value esists or not, is to check.</param>
		/// <returns>
        /// true if the corresponding value exists; otherwise, false.
		/// </returns>
		public bool Has(object o)
		{
			return (this[o] != null);
		}

		/// <summary>
        /// Returns a value indicating whether a value exists in the Session corresponding to the 
        /// specified key.
		/// </summary>
        /// <param name="key">The key for which the corresponding value esists or not is to check.</param>
		/// <returns>
        /// true if the corresponding value exists; otherwise, false.
		/// </returns>
		public bool Has(string key)
		{
			return (this[key] != null);
		}

		/// <summary>
        /// Returns a value indicating whether a value exists in the Session corresponding to the 
        /// specified key (combination of the specified object and string).
		/// </summary>
        /// <param name="o">The object part of the key for which the corresponding value 
        /// esists or not is to check.</param>
        /// <param name="key">The string part of the key for which the corresponding value 
        /// esists or not is to check.</param>
		/// <returns>
        /// true if the corresponding value exists; otherwise, false.
		/// </returns>
		public bool Has(object o, string key)
		{
			if (o == null)
				return false;
			
			return (this[o.ToString() + key] != null);
		}

        /// <summary>
        /// Gets or sets the object in the Session corresponding to the specified key object.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is to get or set.</param>
        /// <returns>
        /// The object in the Session corresponding to the specified key object.
        /// </returns>
		public object this[object o]
		{
			get
			{
				if (o == null)
					return null;

				return this[o.ToString()];
			}
			
			set
			{
				if (o != null)
					this[o.ToString()] = value;
			}
		}

		/// <summary>
        /// Gets or sets the value in the Session corresponding to the specified key (combination 
        /// of the specified object and string).
		/// </summary>
		/// <returns>
        /// The value in the Session corresponding to the specified key (combination of the 
        /// specified object and string)
        /// </returns>
		public object this[object o, string key]
		{
			get
			{
				if (o == null)
					return null;

				return this[o.ToString() + key];
			}
			
			set
			{
				if (o != null)
					this[o.ToString() + key] = value;
			}
		}

		/// <summary>
        /// Removes the value from the Session corresponding to the specified key.
		/// </summary>
        /// <param name="key">The key for which the corresponding value is to be removed.</param>
		public void Remove(string key)
		{
			HttpContext.Current.Session.Remove(key);
		}

		/// <summary>
        /// Removes the value from the Session corresponding to the specified key object.
		/// </summary>
        /// <param name="o">The key object for which the corresponding value is to be removed.</param>
        public void Remove(object o)
		{
			if (o != null)
				HttpContext.Current.Session.Remove(o.ToString());
		}

	}
}