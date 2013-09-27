using System.Collections.Generic;
using Adf.Core.State;
using Windows.UI.Xaml;

namespace Adf.WinRT.State
{
    /// <summary>
    /// Represents Application.Current.Resources. 
    /// Provides properties and methods to get, set, remove a value corresponding to a specified key etc.
    /// </summary>
    public class ApplicationResourcesStateProvider : IStateProvider
    {
        private const string Format = "{0}:{1}";

        private static readonly ResourceDictionary state = Application.Current.Resources;

        #region IState Members

        /// <summary>
        /// Returns a value indicating whether a value exists in its <see cref="ResourceDictionary"/> corresponding to the 
        /// specified key object.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// The method is yet to be implemented.
        /// </exception>
        public bool Has(object o)
        {
            return state.ContainsKey(o);
        }

        /// <summary>
        /// Returns a value indicating whether a value exists in its <see cref="ResourceDictionary"/> corresponding to the 
        /// specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// The method is yet to be implemented.
        /// </exception>
        public bool Has(string key)
        {
            return state.ContainsKey(key);
        }

        /// <summary>
        /// Returns a value indicating whether a value exists in its <see cref="ResourceDictionary"/> corresponding to the 
        /// specified key (combination of the specified object and string).
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <param name="key">The string part of the key for which the corresponding value esists or not, is being checked.</param>
        /// <returns>
        /// true if the corresponding value exists; otherwise, false.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// The method is yet to be implemented.
        /// </exception>
        public bool Has(object o, string key)
        {
            return state.ContainsKey(string.Format(Format, o, key));
        }

        /// <summary>
        /// Gets or sets the value corresponding to the specified key object in its <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key object is returned.
        /// </returns>
        public object this[object o]
        {
            get { return (o != null) ? state[o] : null; }
            set { if (o != null) state[o] = value; }
        }

        /// <summary>
        /// Gets or sets the value corresponding to the specified key in its <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the key is returned.
        /// </returns>
        public object this[string key]
        {
            get
            {
                if (key == null) return null;

                object value;
                if (!state.TryGetValue(key, out value))
                {
                    value = key;
                }

                return value;
            }
            set { if (key != null) state[key] = value; }
        }

        /// <summary>
        /// Gets or sets the value corresponding to the specified key (combination of the specified object and string) 
        /// in its <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="o">The object part of the key for which the corresponding value is get or set.</param>
        /// <param name="key">The string part of the key for which the corresponding value is get or set.</param>
        /// <returns>
        /// If the corresponding value exists the value is returned, otherwise the 
        /// key (combination of the specified object and string) is returned.
        /// </returns>
        public object this[object o, string key]
        {
            get
            {
                if (key == null || o == null) return null;

                key = string.Format(Format, o, key);

                object value;
                if (!state.TryGetValue(key, out value))
                {
                    value = null;
                }

                return value;
            }
            set { if ((key != null) & (o != null)) state[string.Format(Format, o, key)] = value; }
        }

        /// <summary>
        /// Removes the value corresponding to the specified key from its <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is to be removed.</param>
        public void Remove(string key)
        {
            if (key != null) state.Remove(key);
        }

        /// <summary>
        /// Removes the value corresponding to the specified key object from its <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="o">The key object for which the corresponding value is to be removed.</param>
        public void Remove(object o)
        {
            if (o != null) state.Remove(o);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
