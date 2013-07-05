using System;
using System.Configuration;
using Adf.Core.State;

namespace Adf.Base.State
{
	/// <summary>
    /// Represents Configuration State Provider. This is used by the <see cref="StateManager"/>.
    /// Provides properties and methods to get, set, remove a value from its configuration corresponding 
    /// to a specified key etc.
	/// </summary>
	public class ConfigStateProvider : IStateProvider
	{
        /// <summary>
        /// Returns the string value corresponding to the specified key from the <appSettings></appSettings> 
        /// section of the configuration file of the application.
        /// </summary>
        /// <param name="s">The key for which the corresponding string value is required.</param>
        /// <returns>
        /// The string value corresponding to the specified key.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private string Setting(string s)
		{
			return ConfigurationManager.AppSettings[s];
		}

        /// <summary>
        /// Returns a value indicating whether a value exists in the <appSettings></appSettings> 
        /// section of configuration file of the application corresponding to the specified 
        /// key object.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
        public bool Has(object o)
		{
			if (o == null)
				return false;
        	
			return (!string.IsNullOrEmpty(Setting(o.ToString())));
		}

        /// <summary>
        /// Returns a value indicating whether a value exists in the <appSettings></appSettings> 
        /// section of the configuration file of the application corresponding to the 
        /// specified key.
        /// </summary>
        /// <param name="key">The key object for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
        public bool Has(string key)
		{
			return (!string.IsNullOrEmpty(Setting(key)));
		}

        /// <summary>
        /// Returns a value indicating whether a value exists in the <appSettings></appSettings> 
        /// section of the configuration file of the application corresponding to the 
        /// specified key (combination of the specified object and string).
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <param name="key">The string part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
		public bool Has(object o, string key)
		{
			if (o == null)
				return false;

			return (!string.IsNullOrEmpty(Setting(o.ToString() + "." + key)));
		}

        /// <summary>
        /// Returns the value corresponding to the specified key from the <appSettings></appSettings> 
        /// section of the configuration file of the application.
        /// </summary>
        /// <param name="s">The key for which the corresponding value is required.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key is returned.
        /// </returns>
        private object GetSetting(string s)
		{
            return Setting(s) ?? s;
		}

        /// <summary>
        /// Gets or sets the value corresponding to the specified key object in the <appSettings></appSettings> 
        /// section of the configuration file of the application.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key object is 
        /// returned.
        /// </returns>
        /// <exception cref="System.NotSupportedException">
        /// The setting is not supported.
        /// </exception>
		public object this[object o]
		{
			get { return GetSetting(o.ToString()); }
            set { throw new NotSupportedException(); }
		}

        /// <summary>
        /// Gets or sets the value corresponding to the specified key in the <appSettings></appSettings> 
        /// section of the configuration file of the application.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key is returned.
        /// </returns>
        /// <exception cref="System.NotSupportedException">
        /// The setting is not supported.
        /// </exception>
		public object this[string key]
		{
			get { return GetSetting(key); }
            set { throw new NotSupportedException(); }
		}

        /// <summary>
        /// Gets or sets the value corresponding to the specified key (combination of the specified object and string) in 
        /// the <appSettings></appSettings> section of the configuration file of the application.
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value is get or set.</param>
        /// <param name="key">The string part of the key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the 
        /// key (combination of the specified object and string) is returned.
        /// </returns>
        /// <exception cref="System.NotSupportedException">
        /// The setting is not supported.
        /// </exception>
		public object this[object o, string key]
		{
			get { return GetSetting(o + "." + key); }
            set { throw new NotSupportedException(); }
		}

        /// <summary>
        /// Removes the value corresponding to the specified key from the <appSettings></appSettings> 
        /// section of the configuration file of the application.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is to be removed.</param>
        /// <exception cref="System.NotSupportedException">
        /// The method is not supported.
        /// </exception>
		public void Remove(string key)
		{
            throw new NotSupportedException();
		}

        /// <summary>
        /// Removes the value corresponding to the specified key object from the <appSettings></appSettings> 
        /// section of the configuration file of the application using ADF.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is to be removed.</param>
        /// <exception cref="System.NotSupportedException">
        /// The method is not supported.
        /// </exception>
		public void Remove(object o)
		{
			throw new NotSupportedException();
		}

        /// <summary>
        /// Removes all values from the state
        /// </summary>
        public void Clear()
        {
            throw new NotSupportedException();
        }
	}
}
