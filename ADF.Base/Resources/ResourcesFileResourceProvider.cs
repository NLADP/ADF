using System;
using System.Globalization;
using System.IO;
using Adf.Core.Resources;
using Adf.Core.State;

namespace Adf.Base.Resources
{
    /// <summary>
    /// Represents the resource provider for the resource files.
    /// Provides methods to get the string value corresponding to the specified key from the resource files etc.
    /// </summary>
    public class ResourcesFileResourceProvider : IResourceProvider
    {
        /// <summary>
        /// Gets the file based <see cref="System.Resources.ResourceManager"/>.
        /// </summary>
        /// <returns>
        /// The file based <see cref="System.Resources.ResourceManager"/>.
        /// </returns>
        private static System.Resources.ResourceManager Resource
        {
            get
            {
                return System.Resources.ResourceManager.CreateFileBasedResourceManager(
                    StateManager.Settings["ResourceFile"] as string,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StateManager.Settings["ResourceDir"] as string),
                    null);
            }
        }

        #region IResourceProvider Members
        /// <summary>
        /// Returns the value from the resource file corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <returns>
        /// The corresponding value for the specified key.
        /// </returns>
        public string GetString(string key)
        {
            System.Resources.ResourceManager resource = Resource;

            string value = resource.GetString(key);

            resource.ReleaseAllResources();

            return value;
        }

        /// <summary>
        /// Returns a value from the resource file corresponding to the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <param name="culture">The specified <see cref="System.Globalization.CultureInfo"/>.</param>
        /// <returns>
        /// The corresponding value for the specified key and <see cref="System.Globalization.CultureInfo"/>.
        /// </returns>
        public string GetString(string key, CultureInfo culture)
        {
            System.Resources.ResourceManager resource = Resource;

            string value = resource.GetString(key, culture);

            resource.ReleaseAllResources();

            return value;
        }

        #endregion
    }
}
