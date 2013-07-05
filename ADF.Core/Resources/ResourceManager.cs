using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Adf.Core.Objects;
using Adf.Core.Extensions;
using Adf.Core.State;

namespace Adf.Core.Resources
{
    /// <summary>
    /// Represents manager for resource related activities.
    /// Provides method to get the values from a resource.
    /// </summary>
    public static class ResourceManager
    {
        private static IEnumerable<IResourceProvider> _providers;

        private static readonly object _lock = new object();

        /// <summary>
        /// Gets the list of a particular type of resource providers.
        /// </summary>
        /// <returns>
        /// The list of a particular type of resource providers.
        /// </returns>
        private static IEnumerable<IResourceProvider> providers
        {
            get
            {
                lock (_lock) return _providers ?? (_providers = ObjectFactory.BuildAll<IResourceProvider>().ToList());
            }
        }

        private static string _resourceNotFoundFormat;

        private static string ResourceNotFoundFormat
        {
            get
            {
                return _resourceNotFoundFormat ?? (_resourceNotFoundFormat =
                                                   StateManager.Settings.Has("ResourceNotFoundFormat")
                                                       ? StateManager.Settings["ResourceNotFoundFormat"].ToString()
                                                       : "{0}");
            }
        }

        /// <summary>
        /// Returns the value from a resource corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <param name="cultureInfo"></param>
        /// <returns>
        /// The corresponding value of the specified key.
        /// </returns>
        public static string GetString(string key, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(key)) return key;

            string value = null;

            foreach (IResourceProvider provider in providers)
            {
                value = provider.GetString(key, cultureInfo);

                if (value != null) break;
            }

            return value ?? string.Format(ResourceNotFoundFormat, key);
        }

        /// <summary>
        /// Returns the value from a resource corresponding to the specified key.
        /// </summary>
        /// <param name="key">The key for which the corresponding value is required.</param>
        /// <returns>
        /// The corresponding value of the specified key.
        /// </returns>
        public static string GetString(string key)
        {
            return GetString(key, CultureInfo.CurrentUICulture);
        }
    }

    public static class ResourceExtensions
    {
        public static string GetString(this string key)
        {
            return key.IsNullOrEmpty() ? key : ResourceManager.GetString(key);
        }
    }
}
