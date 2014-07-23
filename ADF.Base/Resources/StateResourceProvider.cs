using System.Globalization;
using Adf.Core.Resources;
using Adf.Core.State;

namespace Adf.Base.Resources
{
    /// <summary>
    /// Represents the resource provider for the States.
    /// Provides methods to get the string value corresponding to the specified key from the States etc.
    /// </summary>
    public class StateResourceProvider : IResourceProvider
    {
        #region IResourceProvider Members
        /// <summary>
        /// Returns the value from the State corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <returns>
        /// The corresponding value for the specified key.
        /// </returns>
        public string GetString(string key)
        {
            var value = StateManager.Settings[key] as string;

            return value == key ? null : value;
        }

        /// <summary>
        /// Returns the value from the State corresponding to the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <param name="culture">The specified <see cref="System.Globalization.CultureInfo"/>.</param>
        /// <returns>
        /// The corresponding value for the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// The method is yet to be implemented.
        /// </exception>
        public string GetString(string key, CultureInfo culture)
        {
            return GetString(key);

        }

        #endregion
    }
}
