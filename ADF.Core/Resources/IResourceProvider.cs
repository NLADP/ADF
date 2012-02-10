using System.Globalization;

namespace Adf.Core.Resources
{
    /// <summary>
    /// Defines methods that a value type or class implements to get the string value corresponding to the 
    /// specified key from the resource files.
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Returns a value from the resource file corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <returns>
        /// The corresponding value for the specified key.
        /// </returns>
        string GetString(string key);

        /// <summary>
        /// Returns a value from the resource file corresponding to the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <param name="culture">The specified <see cref="System.Globalization.CultureInfo"/>.</param>
        /// <returns>
        /// The corresponding value for the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </returns>
        string GetString(string key, CultureInfo culture);
    }
}
